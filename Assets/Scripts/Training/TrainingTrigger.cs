using UnityEngine;

namespace Training
{
    public class TrainingTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TrainingManager.StartTraining(this.gameObject);
            }
        }
    }
}
