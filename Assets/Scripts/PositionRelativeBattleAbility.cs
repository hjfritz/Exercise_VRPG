using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelativeBattleAbility : BattleAbility
{
    protected GameObject mainCameraObj;
    [SerializeField] protected GameObject targetsPrefab;
    [SerializeField] private Vector3 relativeTransform;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        mainCameraObj = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        targetsPrefab = Instantiate(targetsPrefab, xrOrigin.transform);
        SetPrefabPostion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrefabPostion()
    {
        targetsPrefab.transform.parent = xrOrigin.transform;
        targetsPrefab.transform.position = relativeTransform + mainCameraObj.transform.position;
        float yRotation = mainCameraObj.transform.eulerAngles.y;
        targetsPrefab.transform.eulerAngles = new Vector3(0, yRotation, 0);
        targetsPrefab.transform.parent = null;
    }

    public override void ExecuteAction()
    {
        SetPrefabPostion();
    }
}