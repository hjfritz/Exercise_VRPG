using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelativeBattleAbility : BattleAttackAbility
{
    protected GameObject mainCameraObj;
    [SerializeField] protected GameObject targetsPrefab;
    protected Vector3 relativeTransform;
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
        targetsPrefab.transform.parent = xrOrigin.transform.GetChild(0);
        float yRotation = mainCameraObj.transform.localEulerAngles.y;
        targetsPrefab.transform.localEulerAngles = new Vector3(0, yRotation, 0);
        targetsPrefab.transform.localPosition = relativeTransform + mainCameraObj.transform.localPosition;
        targetsPrefab.transform.parent = null;
    }

    public override void ExecuteAction(Combatant target)
    {
        SetPrefabPostion();
        base.ExecuteAction(target);
    }
    public override void TrainAction()
    {
        SetPrefabPostion();
        base.TrainAction();
    }
}
