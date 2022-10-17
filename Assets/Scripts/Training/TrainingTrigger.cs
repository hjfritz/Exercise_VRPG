using UnityEngine;

namespace Training
{
    public class TrainingTrigger : MonoBehaviour
    {
        private bool entered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!entered)
                {
                    TrainingManager.StartTraining(this.gameObject);
                    entered = true;
                }
            }
        }
    }
}
