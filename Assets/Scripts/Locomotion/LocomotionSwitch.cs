using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Locomotion
{
    public class LocomotionSwitch : MonoBehaviour
    {
        public bool locomotionOn = true;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (locomotionOn)
            {
                this.GetComponent<ElectricOwl>().enabled = true;
                this.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
                this.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            }
            else
            {
                this.GetComponent<ElectricOwl>().enabled = false;
                this.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
                this.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            }
        }
    }
}
