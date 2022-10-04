using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleAbility : MonoBehaviour
{
    public UnityEvent<int> AbilityComplete = new UnityEvent<int>();
    public String DisplayName = "Display Name Not Set";
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecuteAction()
    {
        
    }

    public virtual void FinalizeAction()
    {
        
    }
}