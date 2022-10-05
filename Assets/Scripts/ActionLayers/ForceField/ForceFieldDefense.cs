using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionLayers.ForceField
{
    public class ForceFieldDefense : BattleAbility
    {
        [SerializeField] private GameObject forceField;
        [SerializeField] private GameObject forceFieldLayer;
        [SerializeField] private GameObject actionLayer;
        [SerializeField] private InputActionReference start;
        
        public static int fieldStrength = 0;
        
        private Vector3 _ffScale = new Vector3(.1f, .1f, .01f);
        private bool counting = false;
        private float defenseDuration = 20.0f;
        private float defenseTimer = 0f;
        
        // Start is called before the first frame update
        new void Start()
        {
            forceField.transform.localScale = new Vector3(0,0,0);
            
            
            base.Start();
        }

        public override void ExecuteAction()
        {
            counting = true;
            defenseTimer = defenseDuration;
            actionLayer.transform.position = transform.position;
            fieldStrength = 0;
            forceFieldLayer.SetActive(true);
            base.ExecuteAction();
        }

        // Update is called once per frame
        void Update()
        {
            if (fieldStrength >= 100)
            {
                FinalizeAction();
            }else if (defenseTimer <= 0)
            {
                FinalizeAction();
            }
            else
            {
                defenseTimer -= Time.deltaTime;
                
                _ffScale = new Vector3(.01f * fieldStrength, .01f * fieldStrength, .01f);

                forceField.transform.localScale = _ffScale;
            }
        }
        
        public override void FinalizeAction()
        {
            AbilityComplete.Invoke(fieldStrength);
            Debug.Log("Success!");
            ResetAbility();
        }
        
        public void AddFieldStrength(float handSpeed)
        {
            fieldStrength += (int)handSpeed;
        }
        
        private void ResetAbility()
        {
            counting = false;
            defenseTimer = defenseDuration;
            forceFieldLayer.SetActive(false);
        }
    }
}
