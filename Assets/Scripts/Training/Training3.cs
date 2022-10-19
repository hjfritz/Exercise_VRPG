using System.Collections;
using System.Net;
using ActionLayers.ForceField;
using Button_UI;
using Locomotion;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Training
{
    public class Training3 : MonoBehaviour
    {
        
        [SerializeField] private GameObject orbA1;
        [SerializeField] private GameObject player;
        
        [SerializeField] private float speed;
        
        //AudioClips
        [SerializeField] private AudioClip intro2;
        [SerializeField] private AudioClip no2;
        [SerializeField] private AudioClip training3;
        [SerializeField] private AudioClip outroA3;

        private GameObject teacher;
        private GameObject currentOrb;
        private bool chosen = false;
        private Animator _animator;
        private bool actionComplete = false;

        private BattleAbility trainingAbility;

        private void Start()
        {
            trainingAbility = FindObjectOfType<PlayerCombatant>().GetComponent<ForceFieldDefense>();
        }
        
        // Update is called once per frame
        void Update()
        {
            if (OptionButtons.ButtonChoice != 0 && TrainingManager.trainingNumber == 4)
            {
                if (OptionButtons.ButtonChoice == 1)
                {
                    ButtonYes();
                }
                else
                {
                    ButtonNo();
                }
                OptionButtons.ResetButtons();
            }
            
            if (actionComplete)
            {
                StartCoroutine(Outro());
                actionComplete = false;
            }
        }

        public void StartTraining3()
        {
            teacher = TrainingManager.currentTeacher;
            _animator = teacher.GetComponentInChildren<Animator>();
            StartCoroutine(PlayIntro());
        }

        IEnumerator PlayIntro()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(intro2);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));

            OptionButtons.ButtonsOn = true;
        }

        public void ButtonYes()
        {
            StartCoroutine(YesOption());
        }
        
        public void ButtonNo()
        {
            StartCoroutine(NoOption());
        }

        IEnumerator YesOption()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(training3);
            _animator.SetBool("ForceFieldTraining", true);
            teacher.GetComponentInChildren<TrainingObjects>().fielding = true;
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            _animator.SetBool("ForceFieldTraining", false);
            teacher.GetComponentInChildren<TrainingObjects>().fielding = false;
            
            trainingAbility.TrainingComplete.AddListener(DetectedAction);
            trainingAbility.TrainAction();
            
        }
        
        private void DetectedAction()
        {
            trainingAbility.TrainingComplete.RemoveListener(DetectedAction);
            StartCoroutine(Outro());
        }
        
        IEnumerator NoOption()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(no2);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            TrainingManager.trainingNumber -= 1;
            
            StartCoroutine(teacher.GetComponentInChildren<TrainingTrigger>().ResetTrainer());
            player.GetComponent<LocomotionSwitch>().locomotionOn = true;
        }

        IEnumerator Outro()
        {
            StartCoroutine(FloatSphereToPlayer());
            
            teacher.GetComponent<AudioSource>().PlayOneShot(outroA3);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            teacher.SetActive(false);
            player.GetComponent<LocomotionSwitch>().locomotionOn = true;
        }
        
        IEnumerator FloatSphereToPlayer()
        {
            float elapsedTime = 0;
            Vector3 startPos = teacher.transform.position + (Vector3.up * 1.5f);
            Vector3 endPos = startPos + (((player.transform.position + (Vector3.up * 1.5f))- startPos) * .75f);

            currentOrb = Instantiate(orbA1, startPos, Quaternion.identity);

            while (elapsedTime < speed)
            {
                currentOrb.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / speed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
