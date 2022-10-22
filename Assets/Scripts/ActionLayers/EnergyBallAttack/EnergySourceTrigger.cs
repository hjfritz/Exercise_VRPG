using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionLayers.EnergyBallAttack
{
    public class EnergySourceTrigger : MonoBehaviour
    {
        [SerializeField] private InputActionReference gripLeft;
        [SerializeField] private InputActionReference gripRight;
        [SerializeField] private bool left = true;
        
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip successSound;
        
        public static bool leftInDestination = false;
        public static bool rightInDestination = false;
        public static bool taskDone = false;

        private bool gripHeld = false;
        private bool startedInBox = false;
        private bool inBox = false;

        // Start is called before the first frame update
        void Start()
        {
            if (left)
            {
                gripLeft.action.started += LeftGripped;
                gripLeft.action.canceled += LeftStoppedGrip;
            }
            else
            {
                gripRight.action.started += RightGripped;
                gripRight.action.canceled += RightStoppedGrip;
            }

            if (taskDone && left)
            {
                gripLeft.action.started -= LeftGripped;
                gripLeft.action.canceled -= LeftStoppedGrip;
            }else if (taskDone && !left)
            {
                gripRight.action.started -= RightGripped;
                gripRight.action.canceled -= RightStoppedGrip;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            inBox = true;
            
            if (gripHeld)
            {
                startedInBox = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            inBox = false;
            
            if (!gripHeld)
            {
                startedInBox = false;
            }
        }
        
        private void LeftGripped(InputAction.CallbackContext obj)
        {
            gripHeld = true;
            if (inBox)
            {
                startedInBox = true;
            }
        }
        
        private void LeftStoppedGrip(InputAction.CallbackContext obj)
        {
            if (leftInDestination && startedInBox)
            {
                AddPowerToEnergyBall(10);
                sfx.PlayOneShot(successSound);
            }
            
            gripHeld = false;
            
        }
        
        private void RightGripped(InputAction.CallbackContext obj)
        {
            gripHeld = true;
            if (inBox)
            {
                startedInBox = true;
            }
        }

        private void RightStoppedGrip(InputAction.CallbackContext obj)
        {
            if (rightInDestination && startedInBox)
            {
                AddPowerToEnergyBall(10);
                sfx.PlayOneShot(successSound);
            }
            
            gripHeld = false;
        }
        
        public void AddPowerToEnergyBall(int addedPower)
        {
            EnergyBallAttack.attackPower += addedPower;
            EnergyBallAttack.repCounter++;
        }

    }
}
