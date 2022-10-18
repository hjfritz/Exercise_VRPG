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
    [SerializeField] private float _teleportTime = 1.5f;

    private Vector3 _teleportStart;
    private Vector3 _teleportEnd;
    private bool _isTeleporting;
    private XROrigin _rig;
    private PlayerManager _pm;
    private Camera _camera;
    private float _teleportTimer = 0f;

    private void Start()
    {
        _rig = FindObjectOfType<XROrigin>();
        _pm = FindObjectOfType<PlayerManager>();
        _camera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
    }

    private void BeginTeleport()
    {
        //somewhat magical fix to the remaining prefab positioning bugs.... I wouldn't trust it 
        //https://www.youtube.com/watch?v=EmjBonbATS0
        var rotationAngleY = _rig.transform.rotation.eulerAngles.y - _camera.transform.rotation.eulerAngles.y;
        _rig.transform.Rotate(0, -rotationAngleY, 0);
        
        _teleportStart = _rig.transform.position;
        //small fix until we flatten out combat regions
        _teleportEnd = new Vector3(transform.position.x, _teleportStart.y, transform.position.z);
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
        //There is still an issue if the XR rig leaves and triggers again during the battle, but that seems like
        //something we can solve hen we disable locomotion
        if (other.gameObject == _rig.gameObject)
        {
            StartFight.Invoke();
        
            if (_pm)
            {
                _pm.transform.position = transform.position ;
                _pm.transform.rotation = transform.rotation;
                _pm.currentCombatManager= transform.parent.GetComponent<CombatManager>();
                //this is a hack to check if its the autonomous combat manager, so that both can work in the scene 
                //at the same time while e are transitioning
                if (!_pm.currentCombatManager.GetComponentInChildren<PlayerCombatant>())
                {
                    _pm.menu.SetActive(true);
                }
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
