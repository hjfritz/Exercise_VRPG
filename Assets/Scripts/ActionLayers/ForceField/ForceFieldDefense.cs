using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionLayers.ForceField
{
    public class ForceFieldDefense : BattleAbility
    {
        [SerializeField] private GameObject forceField;
        [SerializeField] private GameObject forceFieldLayer;
        [SerializeField] private GameObject actionLayer;

        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip ambiance;
        
        public static int fieldStrength = 0;
        
        private Vector3 _ffScale = new Vector3(.1f, .1f, .01f);
        private float _defenseDuration = 20.0f;
        private float _defenseTimer = 0f;
        
        // Start is called before the first frame update
        new void Start()
        {
            forceField.transform.localScale = new Vector3(0,0,0);
            abilityDuration = _defenseDuration;
            base.Start();
        }

        public override void ExecuteAction()
        {
            _defenseTimer = _defenseDuration;
            actionLayer.SetActive(true);
            fieldStrength = 0;
            forceFieldLayer.SetActive(true);
            sfx.PlayOneShot(ambiance);
            base.ExecuteAction();
        }

        // Update is called once per frame
        void Update()
        {
            if (fieldStrength >= 100)
            {
                FinalizeAction();
            }else if (_defenseTimer < 0)
            {
                FinalizeAction();
            }
            else
            {
                _defenseTimer -= Time.deltaTime;
                
                _ffScale = new Vector3(.01f * fieldStrength, .01f * fieldStrength, .01f);

                forceField.transform.localScale = _ffScale;
            }
        }
        
        public override void FinalizeAction()
        {
            AbilityComplete.Invoke(fieldStrength);
            ResetAbility();
        }
        
        private void ResetAbility()
        {
            _defenseTimer = _defenseDuration;
            forceFieldLayer.SetActive(false);
            sfx.Stop();
        }
    }
}
