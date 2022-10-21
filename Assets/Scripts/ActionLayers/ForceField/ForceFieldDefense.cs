using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionLayers.ForceField
{
    public class ForceFieldDefense : DefenseAbility
    {
        private GameObject forceField;
        private GameObject forceFieldLayer;
        private GameObject actionLayer;

        private AudioSource sfx;
        [SerializeField] private AudioClip ambiance;
        
        public static int fieldStrength = 0;
        
        private Vector3 _ffScale = new Vector3(.1f, .1f, .01f);
        //private float _defenseDuration = 20.0f;
        private float _defenseTimer = 0f;

        private bool abilityActive = false;
        private bool training = false;
        private int trainingStrength = 50;
        
        protected GameObject mainCameraObj;
        [SerializeField] protected GameObject targetsPrefab;
        protected Vector3 relativeTransform;

        
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();

            mainCameraObj = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            actionLayer = targetsPrefab;
            sfx = actionLayer.GetComponent<AudioSource>();
            forceFieldLayer = targetsPrefab.GetComponentInChildren<CircleDefenseFacing>().gameObject;
            forceField = targetsPrefab.GetComponentInChildren<global::ForceField>().gameObject;
            forceField.transform.localScale = new Vector3(0,0,0);
            actionLayer.SetActive(false);
            
        }

        private void InitializeAction()
        {
            actionLayer.SetActive(true);
            fieldStrength = 0;
            forceFieldLayer.SetActive(true);
            sfx.PlayOneShot(ambiance);
        }

        public override void ExecuteDefense(float duration)
        {
            abilityActive = true;
            _defenseTimer = duration;
            InitializeAction();
        }
        
        public override void TrainAction()
        {
            training = true;
            InitializeAction();
            base.TrainAction();
        }

        // Update is called once per frame
        void Update()
        {
            if (abilityActive || training)
            {
                if (abilityActive && _defenseTimer < 0)
                {
                    FinalizeAction();
                }
                else
                {
                    //Debug.Log($"defense ability running- {_defenseTimer}");
                    _defenseTimer -= Time.deltaTime;
                
                    _ffScale = new Vector3(.01f * fieldStrength, .01f * fieldStrength, .01f);

                    forceField.transform.localScale = _ffScale;

                    defensePower = fieldStrength;
                    //Debug.Log($"defense power- {defensePower}");
                    if (training && defensePower >= trainingStrength)
                    {
                        FinalizeTraining();
                    }
                }
            }
            
        }
        
        public override void FinalizeAction()
        {
            Debug.Log("finalizing");
            AbilityComplete.Invoke();
            ResetAbility();
        }
        public void FinalizeTraining()
        {
            TrainingComplete.Invoke();
            ResetAbility();
        }
        
        private void ResetAbility()
        {
            abilityActive = false;
            training = false;
            actionLayer.SetActive(false);
            defensePower = 0;
            sfx.Stop();
        }
    }
}
