using UnityEngine;

namespace ActionLayers.EnergyBallAttack
{
    public class EnergyBallAttack : BattleAbility
    {
        [SerializeField] private GameObject WholeLayer;
        [SerializeField] private GameObject triggerLayer;
        [SerializeField] private GameObject energyBallLayer;
        [SerializeField] private GameObject energyBall;

        public static int attackPower = 5;
        
        private float _attackDuration = 20.0f;
        private float _attackTimer = 0f;
        private bool counting = false;
    
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            abilityDuration = _attackDuration;
            WholeLayer.SetActive(false);
        }
        
        public override void ExecuteAction()
        {
            _attackTimer = _attackDuration;
            attackPower = 0;
            counting = true;
            energyBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            energyBall.transform.position = energyBallLayer.transform.position;
            WholeLayer.SetActive(true);
            triggerLayer.SetActive(true);
            base.ExecuteAction();
        }

        // Update is called once per frame
        void Update()
        {
            if (counting)
            {
                if (_attackTimer < 0)
                {
                    FinalizeAction();
                    counting = false;
                }
                else
                {
                    _attackTimer -= Time.deltaTime;
                    var psEmission = energyBall.GetComponent<ParticleSystem>().emission;
                    psEmission.rateOverTime = attackPower;
                }
            }
        }
        
        public override void FinalizeAction()
        {
            AbilityComplete.Invoke(attackPower);
            energyBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * 60);
            ResetAbility();
        }
        
        private void ResetAbility()
        {
            _attackTimer = _attackDuration;
            triggerLayer.SetActive(false);
        }
    }
}
