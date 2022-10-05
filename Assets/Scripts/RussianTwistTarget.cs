using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RussianTwistTarget : MonoBehaviour
{
    public UnityEvent<int> TwoHandTrigger;
    public int id;

    public int numHandsInTrigger = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            numHandsInTrigger++;
        }

        if (numHandsInTrigger == 2)
        {
            TwoHandTrigger.Invoke(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            numHandsInTrigger--;
        }
    }

    public void ResetTarget()
    {
        numHandsInTrigger = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
