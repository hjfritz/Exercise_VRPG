using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrefabHeightAdjust : MonoBehaviour
{

    private const float DEFAULT_HEIGHT = 2.082f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustHeight()
    {
        Debug.Log($"Main Camera Count - {GameObject.FindGameObjectsWithTag("MainCamera").Length}");
        var deltaHeight = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform.localPosition.y - DEFAULT_HEIGHT;
        Debug.Log($"transform.localPosition = {transform.localPosition}");
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + deltaHeight,
            transform.localPosition.z);
        Debug.Log(gameObject.name);
        Debug.Log($"deltaHeight = {deltaHeight}");
        Debug.Log($"transform.localPosition = {transform.localPosition}");
    }
}
