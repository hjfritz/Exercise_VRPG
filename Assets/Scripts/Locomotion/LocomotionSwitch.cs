using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Locomotion
{
    public class LocomotionSwitch : MonoBehaviour
    {
        //public bool locomotionOn = true;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ToggleLocomotion(bool loc)
        {
            //locomotionOn = loc;
            this.GetComponent<ElectricOwl>().enabled = loc;
            this.GetComponent<ActionBasedSnapTurnProvider>().enabled = loc;
            this.GetComponent<ActionBasedContinuousMoveProvider>().enabled = loc;
        }
    }
}
