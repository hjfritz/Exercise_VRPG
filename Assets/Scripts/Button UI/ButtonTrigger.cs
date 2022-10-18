using System;
using UnityEngine;

namespace Button_UI
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private bool yes = true;
        
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
            if (yes)
                {
                    OptionButtons.ButtonChoice = 1;
                }
                else
                {
                    OptionButtons.ButtonChoice = 2;
                }
        }
    }
}
