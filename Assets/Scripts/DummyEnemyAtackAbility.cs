using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DummyEnemyAtackAbility : BattleAttackAbility
{
    private bool counting = false;
    private float attackDuration = 10.0f;
    private float attackTimer = 0f;
    
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
            else
            {
                attackTimer -= Time.deltaTime;
            }
        } 
    }
    
    public override void ExecuteAction(Combatant target)
    {
        counting = true;
        attackTimer = attackDuration;
        base.ExecuteAction(target);
    }
    public override void FinalizeAction()
    {
        int damage = Mathf.FloorToInt(random.Next(0, 100) * .2f);
        target.TakeMitigatedDamage(damage);
        AbilityComplete.Invoke();
        ResetAbility();
    }
    
    private void ResetAbility()
    {
        counting = false;
        attackTimer = 0.0f;
    }
    
}
