using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrefabHeightAdjust : MonoBehaviour
{

    private const float DEFAULT_PLAYER_HEIGHT = 1.7f;

    private Vector3 initialposition;
    private bool heightInitialized = false;
    
    private int adjustFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (adjustFrames > 0)
        {
            var deltaHeight = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform.localPosition.y - DEFAULT_PLAYER_HEIGHT;
            transform.localPosition = new Vector3(initialposition.x, initialposition.y + deltaHeight, initialposition.z);
            adjustFrames--;
        }
    }

    public void AdjustHeight()
    {
        if (!heightInitialized)
        {
            initialposition = transform.localPosition;
            heightInitialized = true;
        }

        adjustFrames = 4;

    }

}
