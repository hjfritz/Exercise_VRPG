using UnityEngine;

namespace ForceField
{
    public class ForceFieldDefense : BattleAbility
    {
        
        [SerializeField] private GameObject forceField;
        
        public static int fieldStrength = 0;
        private Vector3 _ffScale = new Vector3(.1f, .1f, .01f);
        
        // Start is called before the first frame update
        new void Start()
        {
            forceField.transform.localScale = _ffScale;
        }

        // Update is called once per frame
        void Update()
        {
            _ffScale = new Vector3(.01f * fieldStrength, .01f * fieldStrength, .01f);

            forceField.transform.localScale = _ffScale;
            
            if (fieldStrength >= 100)
            {
                Success();
            }
        }
        
        public void AddFieldStrength(float handSpeed)
        {
            fieldStrength += (int)handSpeed;
        }
        
        private void Success()
        {
            Debug.Log("Success!");
            AbilityComplete.Invoke(fieldStrength);
        }
    }
}
