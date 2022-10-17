using UnityEngine;
using UnityEngine.Events;

namespace Training
{
    public class TrainingManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent training1;
        [SerializeField] private UnityEvent training2;
        [SerializeField] private UnityEvent training3;
        [SerializeField] private UnityEvent training4;
        [SerializeField] private UnityEvent training5;

        private int trainingNumber = 1;
        public static bool training = false;
        public static GameObject currentTeacher = null;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (training == true)
            {
                Training();
                training = false;
            }
        }

        public static void StartTraining(GameObject teacher)
        {
            currentTeacher = teacher;
            training = true;
        }

        private void Training()
        {
            if (trainingNumber == 1)
            {
                training1.Invoke();
                trainingNumber += 1;
            }else if (trainingNumber == 2)
            {
                training2.Invoke();
                trainingNumber += 1;
            }else if (trainingNumber == 3)
            {
                training3.Invoke();
                trainingNumber += 1;
            }else if (trainingNumber == 4)
            {
                training4.Invoke();
                trainingNumber += 1;
            }else if (trainingNumber == 5)
            {
                training5.Invoke();
                trainingNumber += 1;
            }else if (trainingNumber == 6)
            {
                GameManager.OpenPortal();
            }
        }
    }
}
