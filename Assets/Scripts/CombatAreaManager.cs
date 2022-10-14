using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class CombatAreaManager : MonoBehaviour
{
    public UnityEvent StartFight;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        
        StartFight.Invoke();
        
        var pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        if (pm)
        {
            pm.transform.position = transform.position ;
            pm.transform.rotation = transform.rotation;
            pm.currentCombatManager= transform.parent.GetComponent<CombatManager>();
            pm.menu.SetActive(true);
            pm.currentCombatManager.StartBattle();
            pm.FightStart.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        if (pm)
        {

            pm.currentCombatManager = null;
            pm.menu.SetActive(false);
            pm.FightEnd.Invoke();
        }
    }
}
