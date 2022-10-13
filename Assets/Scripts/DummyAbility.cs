using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyAbility : BattleAbility
{
    private int repCounter = 0;
    private bool counting = false;
    private float attackDuration = 5.0f;
    private float attackTimer = 0f;

    [SerializeField] private int targetReps = 30;
    [SerializeField] private InputActionReference buttonRef;
    // Start is called before the first frame update
    new void Start()
    {
        buttonRef.action.started += CountRep;
        DisplayName = "Dummy Ability";
        abilityDuration = attackDuration;
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
    
    public override void ExecuteAction()
    {
        SetRepsWithDifficulty();
        counting = true;
        attackTimer = attackDuration;
        base.ExecuteAction();
    }

    public override void FinalizeAction()
    {
        int attackPower = (int)(Mathf.Min((float)repCounter / (float)targetReps, 1f) * 100);
        AbilityComplete.Invoke(attackPower);
        ResetAbility();
    }

    private void CountRep(InputAction.CallbackContext obj)
    {
        if (counting)
        {
            repCounter++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counting && attackTimer < 0)
        {
            FinalizeAction();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void ResetAbility()
    {
        counting = false;
        attackTimer = 0.0f;
        repCounter = 0;
    }
}
