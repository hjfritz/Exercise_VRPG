using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatAbility : BattleAttackAbility
{
    private int repCounter = 0;
    private int trainingReps = 3;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;

    private bool training = false;

    private bool squatting;
    private float squatThresholdHeight;
    private Transform head;
    
    [SerializeField] private int targetReps = 12;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;
    new void Start()
    {
        DisplayName = "Squat Ability";
        abilityDuration = attackDuration;
        base.Start();
    }

    private void InitializeAction()
    {
        head = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;
        squatThresholdHeight = head.transform.localPosition.y * .85f;
        Debug.Log($"head height set to {head.transform.localPosition.y}");
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
    
    public void FinalizeTraining()
    {
        TrainingComplete.Invoke();
        ResetAbility();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (counting || training)
        {
            if (counting && attackTimer < 0)
            {
                FinalizeAction();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            
                if (head.transform.localPosition.y < squatThresholdHeight)
                {
                    if (squatting == false)
                    {
                        squatting = true;
                        repCounter++;
                        target?.TakeMitigatedDamage(playerRepDamage);
                        sfx.PlayOneShot(repCountClip);
                        if (training && repCounter == trainingReps)
                        {
                            FinalizeTraining();
                        }
                    }
                }
                else
                {
                    squatting = false;
                }
            }
        }
       
    }

    private void ResetAbility()
    {
        counting = false;
        squatting = false;
        training = false;
        attackTimer = 0.0f;
        repCounter = 0;
    }
}
