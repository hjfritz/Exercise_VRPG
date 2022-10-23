using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RussianTwistAbility : BattleAttackAbility
{

    private int repCounter = 0;
    private int trainingReps = 4;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;
    private bool training = false;
    

    private RussianTwistTarget[] twistTargets;
    private int currentTargetIndex = -1;

    [SerializeField]private int targetReps = 25;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;

    [SerializeField] protected GameObject targetsPrefab;
    

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        DisplayName = "Russian Twist Ability";
        abilityDuration = attackDuration;
       
        twistTargets = targetsPrefab.GetComponentsInChildren<RussianTwistTarget>();

        foreach (var twistTarget in twistTargets)
        {
            twistTarget.TwoHandTrigger.AddListener(TargetTriggered);
        }

        targetsPrefab.gameObject.SetActive(false);
        
    }

    private void TargetTriggered(int targetid)
    {
        //Debug.Log($"currentTargetIndex = {currentTargetIndex},  targetID = {targetid}");
        if (currentTargetIndex == -1 || targetid == currentTargetIndex)
        {
            currentTargetIndex = targetid;
            repCounter++;
            target?.TakeMitigatedDamage(playerRepDamage);
            sfx.PlayOneShot(repCountClip);
            ToggleCurrentTarget();
            if (training && repCounter == trainingReps)
            {
                FinalizeTraining();
            }
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

    private void  InitializeAction()
    {
        targetsPrefab.GetComponent<TargetPrefabHeightAdjust>().AdjustHeight();
        targetsPrefab.SetActive(true);
    }

    public override void ExecuteAction(Combatant target)
    {
        counting = true;
        attackTimer = attackDuration;
        InitializeAction();
        base.ExecuteAction(target);
    }
    
    public override void TrainAction()
    {
        training = true;
        InitializeAction();
        base.TrainAction();
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
    
    public void FinalizeTraining()
    {
        TrainingComplete.Invoke();
        ResetAbility();
    }

    private void ResetAbility()
    {
        counting = false;
        training = false;
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
