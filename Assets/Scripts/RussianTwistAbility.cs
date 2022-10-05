using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianTwistAbility : BattleAbility
{
    
    private int repCounter = 0;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;
    
    private int targetReps = 25;

    private RussianTwistTarget[] twistTargets;
    private int currentTargetIndex = -1;
    [SerializeField] private Transform floor;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;

    [SerializeField] private GameObject targetsPrefab;
    [SerializeField] private Transform xrOrigin;
    
    // Start is called before the first frame update
    new void Start()
    {
        DisplayName = "Russian Twist Ability";
        targetsPrefab = Instantiate(targetsPrefab, xrOrigin);
        targetsPrefab.transform.position = new Vector3(0.525f, .1f, -0.179f);
        twistTargets = targetsPrefab.GetComponentsInChildren<RussianTwistTarget>();
        
        foreach (var twistTarget in twistTargets)
        {
            twistTarget.TwoHandTrigger.AddListener(TargetTriggered);
        }
        targetsPrefab.gameObject.SetActive(false);
        base.Start();
    }

    private void TargetTriggered(int targetid)
    {
        //Debug.Log($"currentTargetIndex = {currentTargetIndex},  targetID = {targetid}");
        if (currentTargetIndex == -1  || targetid == currentTargetIndex)
        {
            currentTargetIndex = targetid;
            repCounter++;
            sfx.PlayOneShot(repCountClip);
            ToggleCurrentTarget();
        }
    }

    public void ToggleCurrentTarget()
    {
        if (currentTargetIndex == 0)
        {
            currentTargetIndex = 1;
        }
        else
        {
            currentTargetIndex = 0;
        }
    }

    public override void ExecuteAction()
    {
        counting = true;
        attackTimer = attackDuration;
        targetsPrefab.SetActive(true);
        
        base.ExecuteAction();
    }
    
    public override void FinalizeAction()
    {
        int attackPower = (int)(Mathf.Min((float)repCounter / (float)targetReps, 1f) * 100);
        AbilityComplete.Invoke(attackPower);
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
        currentTargetIndex = -1;
        foreach (var twistTarget in twistTargets)
        {
            twistTarget.ResetTarget();
        }
        targetsPrefab.SetActive(false);
    }


}
