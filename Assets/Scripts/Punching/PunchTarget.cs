using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PunchTarget : MonoBehaviour
{
    public UnityEvent<int> SingleHitTrigger;
    //public UnityEvent<int> DoubleHitTrigger;
    public int id;
    private string lastentered = "";

    public int hitsinTrigger = 0;

    private void OnTriggerEnter(Collider other)
    {

        hitsinTrigger++;
        SingleHitTrigger.Invoke(hitsinTrigger);
        
        /*if (other.CompareTag("Player") && other.name != lastentered && other.name != "")
        if (hitsinTrigger < 2)
        {
            hitsinTrigger++;
            lastentered = other.name;
            Debug.Log("Verified Collider Name :" + other.name);
            SingleHitTrigger.Invoke(hitsinTrigger);
        }*/

        
        /*if (hitsinTrigger >= 2)
        {
                DoubleHitTrigger.Invoke(hitsinTrigger);

                ResetTarget();
        }*/
        
    }



    public void ResetTarget()
        {
            hitsinTrigger = 0;
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
