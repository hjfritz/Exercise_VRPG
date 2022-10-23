using System;
using UnityEngine;

namespace Button_UI
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private int number = 0;
        
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
            if (number != 0)
            {
                OptionButtons.ButtonChoice = number;
            }
        }
    }
}
