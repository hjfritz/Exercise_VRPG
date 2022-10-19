using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class DummyEnemyAtackAbility : BattleAttackAbility
{
    private bool counting = false;
    private float attackDuration = 10.0f;
    private float attackTimer = 0f;
    [SerializeField] public float repDuration = .9f;
    private float repTimer = 0f;
    
    private Random random = new Random();
    
    new void Start()
    {
        DisplayName = "Enemy Ability";
        abilityDuration = attackDuration;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            if(attackTimer < 0)
            {
                FinalizeAction();
            }
            else if (repTimer <= 0)
            {
                target.TakeMitigatedDamage(enemyRepDamage);
                repTimer = repDuration;
            }
            else
            {
                attackTimer -= Time.deltaTime;
                repTimer -= Time.deltaTime;
            }
        } 
    }
    
    public override void ExecuteAction(Combatant target)
    {
        counting = true;
        attackTimer = attackDuration;
        repTimer = repDuration;
        base.ExecuteAction(target);
    }
    public override void FinalizeAction()
    {
        AbilityComplete.Invoke();
        ResetAbility();
    }
    
    private void ResetAbility()
    {
        counting = false;
        attackTimer = 0.0f;
    }
    
}
