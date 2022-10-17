using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianTwistAbility : PositionRelativeBattleAbility
{

    private int repCounter = 0;
    private bool counting = false;
    private float attackDuration = 15.0f;
    private float attackTimer = 0f;

    

    private RussianTwistTarget[] twistTargets;
    private int currentTargetIndex = -1;

    [SerializeField]private int targetReps = 25;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip repCountClip;

    

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        DisplayName = "Russian Twist Ability";
        abilityDuration = attackDuration;
        relativeTransform = new Vector3(0f, -.3f, .1f);
        //relativeTransform = new Vector3(0f, 0f, 0f);
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
            target.TakeMitigatedDamage(1);
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
        targetsPrefab.SetActive(true);
        SetPrefabPostion();
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
