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
    private PlayerManager _pm;
    private float _teleportTimer = 0f;

    private void Start()
    {
        _rig = FindObjectOfType<XROrigin>();
        _pm = FindObjectOfType<PlayerManager>();
    }

    private void BeginTeleport()
    {
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
        //bugfix - ontriggerenter was getting called multiple times during the fight by the colliders on the hands. 
        //There is still an issue if the XR rig leaves and triggers again durring the battle, but that seems like
        //something we can solve hen we disable locomotion
        if (other.gameObject == _rig.gameObject)
        {
            Debug.Log("XRRIg Triggered Enter");
            StartFight.Invoke();
        
            if (_pm)
            {
                _pm.transform.position = transform.position ;
                _pm.transform.rotation = transform.rotation;
                _pm.currentCombatManager= transform.parent.GetComponent<CombatManager>();
                //_pm.menu.SetActive(true);
                _pm.currentCombatManager.StartBattle();
                _pm.FightStart.Invoke();
            }
        
            BeginTeleport();
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        //bugfix - ontriggerexit was getting triggered by hands
        if (other.gameObject == _rig.gameObject)
        {
            Debug.Log("XRRIg Triggered Exit");
            var pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            if (pm)
            {
                pm.currentCombatManager = null;
                pm.menu.SetActive(false);
                pm.FightEnd.Invoke();
            }
        }
    }
    
    
    
}
