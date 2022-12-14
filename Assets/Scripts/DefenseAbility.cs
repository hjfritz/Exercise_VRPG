using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class DefenseAbility : BattleAbility
{
    private bool defending = false;
    private float defenseTimer = 0f;
    public int defensePower = 0;
    
    private System.Random random = new Random();

    new void Start()
    {
        
        DisplayName = "Defense Ability";
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (defending)
        {
            if(defenseTimer < 0.0f)
            {
                Debug.Log($"FINALIZE DEFENSE {gameObject.name}");
                FinalizeAction();
            }
            else
            {
                //Debug.Log($"DEFENSE {gameObject.name} {defenseTimer}");
                defensePower = random.Next(0, 100);
                defenseTimer -= Time.deltaTime;
            } 
        } 
    }
    
    public  override void ExecuteDefense(float duration)
    {
        Debug.Log($"defense duration {duration}");
        defenseTimer = duration;
        defending = true;
        base.ExecuteDefense(duration);
    }
    public override void FinalizeAction()
    {
        ResetAbility();
        AbilityComplete.Invoke();
    }
    
    private void ResetAbility()
    {
        defending = false;
        defenseTimer = 0.0f;
        defensePower = 0;
    }
    
}
