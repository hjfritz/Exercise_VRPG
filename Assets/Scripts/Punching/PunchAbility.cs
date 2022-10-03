using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAbility : BattleAbility
{
    private int repCounter = 0;
    private bool counting = false;
    private float attackTimer = 0f;
    
    [SerializeField] private float attackDuration = 5.0f;   
    
    public GameObject punchingArea;

    public override void ExecuteAction()
    {
        punchingArea.SetActive(true);
        counting = true;
        attackTimer = attackDuration;
    }

    public override void FinalizeAction()
    {
        punchingArea.SetActive(false);
        Debug.Log("Reps :" + repCounter);
        AbilityComplete.Invoke(repCounter);
        
        ResetAbility();
        base.FinalizeAction();
    }


    public void Hit()
    {
        if (counting)
        {
            repCounter++;
        }
    }

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
