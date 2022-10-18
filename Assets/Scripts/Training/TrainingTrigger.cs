using UnityEngine;

namespace Training
{
    public class TrainingTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        [SerializeField] private ParticleSystem ps;

        private bool entered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!entered)
                {
                    this.transform.LookAt(new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z));
                    ps.Play();
                    model.SetActive(true);
                    other.GetComponentInParent<ElectricOwl>().enabled = false;
                    TrainingManager.StartTraining(this.gameObject);
                    entered = true;
                }
            }
        }
    }
}
