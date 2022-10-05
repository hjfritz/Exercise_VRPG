using UnityEngine;
using UnityEngine.InputSystem;

namespace ForceField
{
    public class ForceFieldDefense : BattleAbility
    {
        
        [SerializeField] private GameObject forceField;
        [SerializeField] private InputActionReference start;
        
        public static int fieldStrength = 0;
        
        private Vector3 _ffScale = new Vector3(.1f, .1f, .01f);
        private bool counting = false;
        private float defenseDuration = 20.0f;
        private float defenseTimer = 0f;
        
        // Start is called before the first frame update
        new void Start()
        {
            forceField.transform.localScale = _ffScale;
            
            base.Start();

            start.action.performed += TestGame;
        }

        private void TestGame(InputAction.CallbackContext obj)
        {
            ExecuteAction();
        }

        public override void ExecuteAction()
        {
            counting = true;
            defenseTimer = defenseDuration;
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
            fieldStrength = 0;
        }
    }
}
