using UnityEngine;

namespace ActionLayers.EnergyBallAttack
{
    public class EnergyBallAttack : BattleAttackAbility
    {
        private GameObject WholeLayer;
        private GameObject triggerLayer;
        private GameObject energyBallLayer;
        private GameObject energyBall;

        public static int attackPower = 5;
        
        private float _attackDuration = 20.0f;
        private float _attackTimer = 0f;
        private bool counting = false;
        
        private bool training = false;
        public static int repCounter = 0;
        private int trainingReps = 4;
        
        [SerializeField] protected GameObject targetsPrefab;
    
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            abilityDuration = _attackDuration;

            WholeLayer = targetsPrefab;
            triggerLayer = targetsPrefab.GetComponentInChildren<TriggersLayer>().gameObject;
            energyBallLayer = targetsPrefab.GetComponentInChildren<EnergyBallLayer>().gameObject;
            energyBall = energyBallLayer.GetComponentInChildren<ParticleSystem>().gameObject;

            WholeLayer.SetActive(false);
        }
        
        private void  InitializeAction()
        {
            targetsPrefab.GetComponent<TargetPrefabHeightAdjust>().AdjustHeight();
            energyBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            energyBall.transform.position = energyBallLayer.transform.position;
            EnergySourceTrigger.taskDone = false;
            WholeLayer.SetActive(true);
            triggerLayer.SetActive(true);
        }
        
        public override void ExecuteAction(Combatant target)
        {
            _attackTimer = _attackDuration;
            attackPower = 0;
            counting = true;
            InitializeAction();
            base.ExecuteAction(target);
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
            if (counting || training)
            {
                if (counting && _attackTimer < 0)
                {
                    FinalizeAction();
                }
                else
                {
                    _attackTimer -= Time.deltaTime;
                    var psEmission = energyBall.GetComponent<ParticleSystem>().emission;
                    psEmission.rateOverTime = attackPower;
                    if (training && repCounter == trainingReps)
                    {
                        FinalizeTraining();
                    }
                }
            }
        }
        
        public override void FinalizeAction()
        {
            int damage = Mathf.FloorToInt(attackPower * .2f);
            target.TakeMitigatedDamage(damage);
            AbilityComplete.Invoke();
            energyBall.GetComponent<Rigidbody>().AddForce((targetsPrefab.transform.forward).normalized * 60); //make changes below too
            ResetAbility();
        }
        
        public void FinalizeTraining()
        {
            TrainingComplete.Invoke();
            energyBall.GetComponent<Rigidbody>().AddForce((targetsPrefab.transform.forward).normalized * 60); //make changes above too
            ResetAbility();
        }
        
        private void ResetAbility()
        {
            attackPower = 0;
            var psEmission = energyBall.GetComponent<ParticleSystem>().emission;
            psEmission.rateOverTime = attackPower;
            _attackTimer = _attackDuration;
            counting = false;
            training = false;
            repCounter = 0;
            triggerLayer.SetActive(false);
            EnergySourceTrigger.taskDone = true;
        }
    }
}
