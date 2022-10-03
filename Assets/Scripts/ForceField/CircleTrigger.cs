using System;
using UnityEditor.XR.Interaction.Toolkit.AR;
using UnityEngine;

namespace ForceField
{
    public class CircleTrigger : BattleAbility
    {
        [SerializeField] private GameObject diameter;
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject forceField;

        private Vector3 _hand2D;
        private int _fieldStrength = 0;
        
        private bool _inTrigger = false;
        private float _handSpeed;
        private Vector3 _oldPosition;
        
        //Timer variables
        private float _secondTimer = 0;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _oldPosition = transform.position;
            forceField.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //Timers
            _secondTimer += Time.deltaTime;
            
            DisplayName = "Force Field Defense";
            TrackHand();
            
            if (_inTrigger)
            {
                TrackHandSpeed();
                if (_secondTimer > 1f && _handSpeed < 10)
                {
                    AddFieldStrength(_handSpeed);
                    Debug.Log(_fieldStrength);
                    _secondTimer = 0;
                }
            }

            if (_fieldStrength >= 100)
            {
                Success();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _inTrigger = true;
            Debug.Log("In");
        }

        private void OnTriggerExit(Collider other)
        {
            _inTrigger = false;
            _handSpeed = 0;
            Debug.Log("Out");
        }

        private void TrackHand()
        {
            _hand2D = new Vector3(hand.transform.position.x, hand.transform.position.y, diameter.transform.position.z);
            diameter.transform.LookAt(_hand2D);
        }

        private void TrackHandSpeed()
        {
            _handSpeed = Vector3.Distance(_oldPosition, transform.position) * 100f;
            _oldPosition = transform.position;
        }
        
        private void AddFieldStrength(float handSpeed)
        {
            _fieldStrength += (int)handSpeed;
        }

        private void Success()
        {
            Debug.Log("Success!");
            forceField.SetActive(true);
        }
    }
}
