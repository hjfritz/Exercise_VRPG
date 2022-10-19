using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class BattleAbility : MonoBehaviour
{
    public UnityEvent AbilityComplete = new UnityEvent();
    public UnityEvent TrainingComplete = new UnityEvent();
    public String DisplayName = "Display Name Not Set";
    public float abilityDuration;
    protected Combatant target;
    protected XROrigin xrOrigin;
    protected PlayerManager playerManager;
    protected int playerRepDamage;
    protected int enemyRepDamage;

    // Start is called before the first frame update
    protected void Start()
    {
        xrOrigin = FindObjectOfType<XROrigin>();
        playerManager = FindObjectOfType<PlayerManager>();
        playerRepDamage = playerManager.MAX_DIFFICULTY - playerManager.difficulty;
        enemyRepDamage = playerManager.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecuteAction(Combatant target)
    {
        this.target = target;
    }
    
    public virtual void ExecuteDefense(float duration)
    {
        
    }

    public virtual void FinalizeAction()
    {
        
    }

    public virtual void TrainAction()
    {
        
    }

}
