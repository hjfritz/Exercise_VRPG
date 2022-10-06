using System;
using UnityEngine;

namespace ActionLayers.EnergyBallAttack
{
    public class EnergyDestinationTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHand")
            {
                Debug.Log("Left hand in destination");
                EnergySourceTrigger.leftInDestination = true;
            }
            
            if (other.name == "RightHand")
            {
                Debug.Log("Right hand in destination");
                EnergySourceTrigger.rightInDestination = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "LeftHand")
            {
                Debug.Log("Left hand out of destination");
                EnergySourceTrigger.leftInDestination = false;
            }
            
            if (other.name == "RightHand")
            {
                Debug.Log("Right hand out of destination");
                EnergySourceTrigger.rightInDestination = false;
            }
        }
    }
}
