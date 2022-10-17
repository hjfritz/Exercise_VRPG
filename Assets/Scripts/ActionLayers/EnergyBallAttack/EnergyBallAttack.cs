using UnityEngine;

namespace ActionLayers.EnergyBallAttack
{
    public class EnergyBallAttack : PositionRelativeBattleAbility
    {
        private GameObject WholeLayer;
        private GameObject triggerLayer;
        private GameObject energyBallLayer;
        private GameObject energyBall;

        public static int attackPower = 5;
        
        private float _attackDuration = 20.0f;
        private float _attackTimer = 0f;
        private bool counting = false;
    
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            abilityDuration = _attackDuration;
            relativeTransform = new Vector3(0f, -.5f, .2f);
            //relativeTransform = new Vector3(0f, 0f, 0f);

            WholeLayer = targetsPrefab;
            triggerLayer = targetsPrefab.GetComponentInChildren<TriggersLayer>().gameObject;
            energyBallLayer = targetsPrefab.GetComponentInChildren<EnergyBallLayer>().gameObject;
            energyBall = energyBallLayer.GetComponentInChildren<ParticleSystem>().gameObject;

            WholeLayer.SetActive(false);
        }
        
        public override void ExecuteAction(Combatant target)
        {
            _attackTimer = _attackDuration;
            attackPower = 0;
            counting = true;
            energyBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            energyBall.transform.position = energyBallLayer.transform.position;
            WholeLayer.SetActive(true);
            triggerLayer.SetActive(true);
            base.ExecuteAction(target);
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
            int damage = Mathf.FloorToInt(attackPower * .2f);
            target.TakeMitigatedDamage(damage);
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
