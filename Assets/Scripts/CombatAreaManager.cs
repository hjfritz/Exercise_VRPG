using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class CombatAreaManager : MonoBehaviour
{
    public UnityEvent StartFight;
    
    [SerializeField] XROrigin _locomoteRig;
    [SerializeField] private XROrigin _battleRig;
    private PlayerManager _pm;

    private CombatManager[] _combatManagers;
    private int currentBattleIndex = 0;
   
    private void Start()
    {
        _pm = FindObjectOfType<PlayerManager>(true);
        _combatManagers = GetComponentsInChildren<CombatManager>(true);
    }

    private void TeleportToBattle()
    {
        _locomoteRig.gameObject.SetActive(false);
        _battleRig.gameObject.SetActive(true);
    }
    
    private void TeleportToLevel()
    {
        _locomoteRig.gameObject.SetActive(true);
        _battleRig.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void TriggerCombat()
    {
        StartFight.Invoke();
        TeleportToBattle();
        _pm.currentCombatManager = _combatManagers[currentBattleIndex];
        _pm.currentCombatManager.gameObject.SetActive(true);
        _pm.menu.SetActive(true);
        _pm.currentCombatManager.StartBattle();
        _pm.FightStart.Invoke();
            
        
    }
    

    
    public void endoffight()
    {
        _pm.currentCombatManager.gameObject.SetActive(false);
        _pm.currentCombatManager = null;
        _pm.menu.SetActive(false);
        currentBattleIndex++;
        _pm.FightEnd.Invoke();
        TeleportToLevel();
    }
    
    
    
}
