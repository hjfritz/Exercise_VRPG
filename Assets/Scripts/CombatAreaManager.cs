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
   
    [SerializeField] private float _teleportTime = 1.5f;

    private Vector3 _teleportStart;
    private Vector3 _teleportEnd;
    private bool _isTeleporting;
    private float _teleportTimer = 0f;

    private Vector3 lookAtDirection;
    
    
    private void Start()
    {
        _pm = FindObjectOfType<PlayerManager>(true);
        _combatManagers = GetComponentsInChildren<CombatManager>(true);
    }

    private void TeleportToBattle()
    {
        Vector3 destination = _pm.currentCombatManager.GetComponentInChildren<CombatTrigger>().transform.position;
        //_locomoteRig.gameObject.SetActive(false);
        //_battleRig.gameObject.SetActive(true);
        
        _teleportStart = _battleRig.transform.position;
        //small fix until we flatten out combat regions
        _teleportEnd = new Vector3(destination.x, _teleportStart.y, destination.z);
        _isTeleporting = true;
        //lookAtDirection = _pm.currentCombatManager.transform - _battleRig.transform;
        
        

    }
    
    private void TeleportToLevel()
    {
        _locomoteRig.gameObject.SetActive(true);
        _battleRig.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isTeleporting)
        {
            _teleportTimer += Time.deltaTime / _teleportTime;

            _battleRig.transform.position = Vector3.Lerp(_teleportStart, _teleportEnd, _teleportTimer);

            if (_teleportTimer > 1f)
            {
                _isTeleporting = false;
                _teleportTimer = 0f;
            }

            var enemyTransform = _pm.currentCombatManager.transform;
            _battleRig.transform.LookAt(new Vector3(enemyTransform.position.x, this.transform.position.y, enemyTransform.position.z));
        }
    }

    public void TriggerCombat()
    {
        StartFight.Invoke();
       
        
        _pm.currentCombatManager = _combatManagers[currentBattleIndex];
        _pm.currentCombatManager.gameObject.SetActive(true);
        TeleportToBattle();
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
        //TeleportToLevel();
    }
    
    
    
}
