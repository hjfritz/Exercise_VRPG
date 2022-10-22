using UnityEngine;

namespace Training
{
    public class AbilitySymbol : MonoBehaviour
    {
        [SerializeField] private GameObject SquatSymbol;
        [SerializeField] private GameObject PunchSymbol;
        [SerializeField] private GameObject ForceFieldSymbol;
        [SerializeField] private GameObject TwistSymbol;
        [SerializeField] private GameObject EnergyBallSymbol;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (TrainingManager.trainingNumber == 2)
            {
                DeactivateAll();
                SquatSymbol.SetActive(true);
            }else if (TrainingManager.trainingNumber == 3)
            {
                DeactivateAll();
                PunchSymbol.SetActive(true);
            }else if (TrainingManager.trainingNumber == 4)
            {
                DeactivateAll();
                ForceFieldSymbol.SetActive(true);
            }else if (TrainingManager.trainingNumber == 5)
            {
                DeactivateAll();
                TwistSymbol.SetActive(true);
            }else if (TrainingManager.trainingNumber == 6)
            {
                DeactivateAll();
                EnergyBallSymbol.SetActive(true);
            }
            else
            {
                DeactivateAll();
            }
        }

        private void DeactivateAll()
        {
            SquatSymbol.SetActive(false);
            PunchSymbol.SetActive(false);
            ForceFieldSymbol.SetActive(false);
            TwistSymbol.SetActive(false);
            EnergyBallSymbol.SetActive(false);
        }
    }
}
