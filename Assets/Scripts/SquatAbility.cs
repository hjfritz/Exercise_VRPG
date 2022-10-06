using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatAbility : BattleAbility
{
    private int repCounter = 0;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;
    
    private int targetReps = 12;

    private bool squatting;
    private float squatThresholdHeight;
    [SerializeField] private Transform head;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;
    new void Start()
    {
        DisplayName = "Squat Ability";
        abilityDuration = attackDuration;
        base.Start();
    }
    
    public override void ExecuteAction()
    {
        counting = true;
        attackTimer = attackDuration;
        squatThresholdHeight = head.transform.position.y * .85f;
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
            
                if (head.transform.position.y < squatThresholdHeight)
                {
                    if (squatting == false)
                    {
                        squatting = true;
                        repCounter++;
                        sfx.PlayOneShot(repCountClip);
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
        attackTimer = 0.0f;
        repCounter = 0;
    }
}
