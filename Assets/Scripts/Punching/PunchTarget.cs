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

        Debug.Log("Collider Name :" + other.name);

        //if (other.CompareTag("Player") && other.name != lastentered && other.name != "")
        if (numHandsInTrigger < 2)
        {
            numHandsInTrigger++;
            lastentered = other.name;
            Debug.Log("Verified Collider Name :" + other.name);
            SingleHitTrigger.Invoke(numHandsInTrigger);
        }

        
     

        if (numHandsInTrigger >= 2)
        {
                DoubleHitTrigger.Invoke(numHandsInTrigger);
                numHandsInTrigger = 0;
                ResetTarget();
        }
        
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
