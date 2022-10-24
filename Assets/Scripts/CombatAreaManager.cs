using System;
using System.Collections;
using System.Collections.Generic;
using Locomotion;
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

    [SerializeField] private float _teleportTime = 1.5f;

    private Vector3 _teleportStart;
    private Vector3 _teleportEnd;
    private bool _isTeleporting;
    private float _teleportTimer = 0f;

    private Vector3 lookAtDirection;

    [SerializeField] private AudioSource BGMsource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip combatMusic;

    private void Start()
    {
        _pm = FindObjectOfType<PlayerManager>(true);
        BGMsource.clip = backgroundMusic;
        BGMsource.Play();
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
            _battleRig.transform.LookAt(new Vector3(enemyTransform.position.x, enemyTransform.position.y, enemyTransform.position.z));
        }
    }

    public void TriggerCombat(CombatManager combatManager)
    {
        StartFight.Invoke();

        _pm.currentCombatManager = combatManager;
        _pm.currentCombatManager.gameObject.SetActive(true);
        
        _battleRig.GetComponent<LocomotionSwitch>().ToggleLocomotion(false);
        //"look away" - https://forum.unity.com/threads/whats-the-opposite-of-lookat.392668/
        _pm.currentCombatManager.transform.rotation = Quaternion.LookRotation(_pm.currentCombatManager.transform.position - _battleRig.transform.position);
        
        
        _pm.menu.SetActive(true);
        _pm.currentCombatManager.StartBattle();
        
        _pm.currentCombatManager.GetComponentInChildren<Combatant>().GetComponentInChildren<EnemyANimationManager>().StartAnim();
        _pm.FightStart.Invoke();

        BGMsource.clip = combatMusic;
        BGMsource.Play();


    }
    

    
    public void endoffight()
    {
        _pm.currentCombatManager = null;
        _pm.menu.SetActive(false);
        _pm.FightEnd.Invoke();
        _battleRig.GetComponent<LocomotionSwitch>().ToggleLocomotion(true);
        
        BGMsource.clip = backgroundMusic;
        BGMsource.Play();
    }
    
    
    
}
