using System;
using UnityEngine;

namespace Training
{
    public class TrainingObjects : MonoBehaviour
    {
        [SerializeField] private GameObject punchZone;
        [SerializeField] private GameObject forceField;
        [SerializeField] private GameObject twistBoxes;
        [SerializeField] private GameObject energyFields;

        public bool punching = false;
        public bool fielding = false;
        public bool twisting = false;
        public bool energizing = false;

        private void Update()
        {
            punchZone.SetActive(punching);

            forceField.SetActive(fielding);

            twistBoxes.SetActive(twisting);

            energyFields.SetActive(energizing);
        }
    }
}
