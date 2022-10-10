using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PunchTarget : MonoBehaviour
{
    public UnityEvent<int> SingleHitTrigger;
    public UnityEvent<int> DoubleHitTrigger;
    public int id;
    private string lastentered = "";

    public int numHandsInTrigger = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Player") && other.name != lastentered && other.name != "")
        {
            numHandsInTrigger++;
            lastentered = other.name;
            Debug.Log("Collider Name :" + other.name);
        }

        if (numHandsInTrigger >= 2)
        {
            DoubleHitTrigger.Invoke(numHandsInTrigger);
            ResetTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {
            numHandsInTrigger--;
        }
        */
    }

    public void ResetTarget()
    {
        numHandsInTrigger = 0;
        lastentered = "";
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
