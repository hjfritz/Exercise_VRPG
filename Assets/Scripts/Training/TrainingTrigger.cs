using System;
using System.Collections;
using Locomotion;
using UnityEngine;

namespace Training
{
    public class TrainingTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private ParticleSystem ps2;

        public bool entered = false;
        public bool trainerActive = false;

        private void Update()
        {
            if (trainerActive)
            {
                model.SetActive(true);
            }
            else
            {
                model.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!entered)
                {
                    this.transform.LookAt(new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z));
                    ps2.Stop();
                    ps.Play();
                    trainerActive = true;
                    other.GetComponentInParent<LocomotionSwitch>().ToggleLocomotion(false);
                    TrainingManager.StartTraining(this.gameObject);
                    entered = true;
                }
            }
        }

        public IEnumerator ResetTrainer()
        {
            trainerActive = false;
            yield return new WaitForSeconds(5);
            entered = false;
            ps2.Play();
        }
    }
}
