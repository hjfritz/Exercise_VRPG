using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatAbility : BattleAttackAbility
{
    private int repCounter = 0;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;
    
    

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
        head = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;
        base.Start();
    }
    
    
    private void SetRepsWithDifficulty()
    {
        var pm = transform.parent.GetComponent<PlayerManager>();
        if (pm)
        {
            targetReps *= pm.difficulty;
        }
    }
    public override void ExecuteAction(Combatant target)
    {
        SetRepsWithDifficulty();
        counting = true;
        attackTimer = attackDuration;
        squatThresholdHeight = head.transform.localPosition.y * .85f;
        Debug.Log($"head height set to {head.transform.localPosition.y}");
        base.ExecuteAction(target);
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
            
                if (head.transform.localPosition.y < squatThresholdHeight)
                {
                    if (squatting == false)
                    {
                        squatting = true;
                        repCounter++;
                        target.TakeMitigatedDamage(1);
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
