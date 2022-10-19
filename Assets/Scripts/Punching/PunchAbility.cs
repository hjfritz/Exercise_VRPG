using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PunchAbility : BattleAttackAbility
{
    private int repCounter = 0;
    private bool counting = false;
    private float attackTimer = 0f;
    private PunchTarget _punchTarget;  
    
    [SerializeField] private float attackDuration = 10.0f;
    [SerializeField] private int targetReps = 10;
    [SerializeField] private Transform floor;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;
    [SerializeField] private AudioClip punchClip;
    [SerializeField] private GameObject targetsPrefab;
    [SerializeField] private Transform punchingArea;
    
    // Start is called before the first frame update
    new void Start()
    {
        DisplayName = "Punch Ability";
        abilityDuration = attackDuration;
        
        base.Start();
    }

    private void TargetTriggered(int targethit)
    {
        sfx.PlayOneShot(punchClip);
        repCounter++;
        target.TakeMitigatedDamage(playerRepDamage);
        
        if (repCounter > targetReps)
        {
            sfx.PlayOneShot(repCountClip);
        }

    }

    private void settarget()
    {

        punchingArea = FindObjectOfType<PlayerManager>().currentCombatManager.transform
            .GetComponentInChildren<CombatAreaManager>().transform;
        
        
        targetsPrefab = Instantiate(targetsPrefab, punchingArea);
        targetsPrefab.transform.position = new Vector3(0f , 1.4f, 0f) + punchingArea.position + (punchingArea.forward * 0.5f); //new Vector3(0f , 1.4f, .8f) + p
        //targetsPrefab.transform.rotation = transform.rotation ;// factor 1 / .77 / .44
        _punchTarget = targetsPrefab.GetComponent<PunchTarget>();
        _punchTarget.SingleHitTrigger.AddListener(TargetTriggered);
        //_punchTarget.DoubleHitTrigger.AddListener(TargetTriggered);
        targetsPrefab.gameObject.SetActive(false);
    }


    public override void ExecuteAction(Combatant target)
    {
        settarget();

        counting = true;
        attackTimer = attackDuration;
        targetsPrefab.SetActive(true);
        base.ExecuteAction(target);
    }
    
    public override void FinalizeAction()
    {
        AbilityComplete.Invoke();
        ResetAbility();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            if (attackTimer < 0)
            {
                FinalizeAction();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
       
    }
    

    private void ResetAbility()
    {
        counting = false;
        attackTimer = 0.0f;
        repCounter = 0;
        _punchTarget.ResetTarget();
        targetsPrefab.SetActive(false);
    }

}
