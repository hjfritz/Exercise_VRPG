using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class CombatAreaManager : MonoBehaviour
{
    public UnityEvent StartFight;
    [SerializeField] private float _teleportTime = 1.5f;

    private Vector3 _teleportStart;
    private Vector3 _teleportEnd;
    private bool _isTeleporting;
    private XROrigin _rig;
    private float _teleportTimer = 0f;

    private void BeginTeleport()
    {
        _rig = GameObject.Find("XR Origin").GetComponent<XROrigin>();
        _teleportStart = _rig.transform.position;
        _teleportEnd = transform.position;
        _isTeleporting = true;
    }

    private void Update()
    {
        if (_isTeleporting)
        {
            _teleportTimer += Time.deltaTime / _teleportTime;
            
            _rig.transform.position = Vector3.Lerp(_teleportStart, _teleportEnd, _teleportTimer);

            if (_teleportTimer > 1f)
            {
                _isTeleporting = false;
                _teleportTimer = 0f;
            }
        }
    }
    
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
        
        BeginTeleport();

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
