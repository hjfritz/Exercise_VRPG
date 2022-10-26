using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PunchAbility : BattleAttackAbility
{
    private int repCounter = 0;
    private int trainingReps = 6;
    private bool counting = false;
    private bool training = false;
    private float attackTimer = 0f;
    private PunchTarget _punchTarget;  
    
    [SerializeField] private float attackDuration = 10.0f;
    [SerializeField] private int targetReps = 10;
    [SerializeField] private Transform floor;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;
    [SerializeField] private AudioClip punchClip;
    [SerializeField] private Transform punchingArea;
    
    [SerializeField] protected GameObject targetsPrefab;
    [SerializeField] private HandAnimationController leftFist;
    [SerializeField] private HandAnimationController rightFist;
    
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        DisplayName = "Punch Ability";
        abilityDuration = attackDuration;
        _punchTarget = targetsPrefab.GetComponent<PunchTarget>();
        _punchTarget.SingleHitTrigger.AddListener(TargetTriggered);
        targetsPrefab.gameObject.SetActive(false);
        
    }

    private void TargetTriggered(int targethit)
    {
        sfx.PlayOneShot(punchClip);
        repCounter++;
        target?.TakeMitigatedDamage(playerRepDamage);
        
        if (repCounter > targetReps)
        {
            sfx.PlayOneShot(repCountClip);
        }
        
        if (training && repCounter == trainingReps)
        {
            FinalizeTraining();
        }

    }

    private void InitializeAction()
    {
        targetsPrefab.SetActive(true);
        targetsPrefab.GetComponent<TargetPrefabHeightAdjust>().AdjustHeight();
        leftFist.Fist(true);
        rightFist.Fist(true);
    }
    public override void ExecuteAction(Combatant target)
    {
        counting = true;
        attackTimer = attackDuration;
        base.ExecuteAction(target);
        InitializeAction();
    }
    
    public override void TrainAction()
    {
        training = true;
        base.TrainAction();
        InitializeAction();
    }
    
    public override void FinalizeAction()
    {
        AbilityComplete.Invoke();
        ResetAbility();
    }
    
    public void FinalizeTraining()
    {
        TrainingComplete.Invoke();
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
        training = false;
        attackTimer = 0.0f;
        repCounter = 0;
        leftFist.Fist(false);
        rightFist.Fist(false);
        _punchTarget.ResetTarget();
        targetsPrefab.SetActive(false);
    }

}
