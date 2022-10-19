using System;
using System.Collections;
using System.Net;
using Button_UI;
using Locomotion;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Training
{
    public class Training1 : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject orbA1;

        [SerializeField] private float speed;
        
        //AudioClips
        [SerializeField] private AudioClip intro;
        [SerializeField] private AudioClip yes;
        [SerializeField] private AudioClip no;
        [SerializeField] private AudioClip training;
        [SerializeField] private AudioClip outro;
        

        private GameObject teacher;
        private GameObject currentOrb;
        private bool chosen = false;
        private Animator _animator;
        private bool actionComplete = false;

        private BattleAbility trainingAbility;

        private void Start()
        {
            trainingAbility = FindObjectOfType<PlayerCombatant>().GetComponent<SquatAbility>();
        }

        // Update is called once per frame
        void Update()
        {
            if (OptionButtons.ButtonChoice != 0 && TrainingManager.trainingNumber == 2)
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

        public void StartTraining1()
        {
            teacher = TrainingManager.currentTeacher;
            _animator = teacher.GetComponentInChildren<Animator>();
            StartCoroutine(PlayIntro());
        }

        IEnumerator PlayIntro()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(intro);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            OptionButtons.ButtonsOn = true;
        }

        private void ButtonYes()
        {
            StartCoroutine(YesOption());
        }

        private void ButtonNo()
        {
            StartCoroutine(NoOption());
        }

        IEnumerator YesOption()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(yes);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            _animator.SetBool("SquatTraining", true);
            teacher.GetComponent<AudioSource>().PlayOneShot(training);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            _animator.SetBool("SquatTraining", false);
            
            //Insert Squat ability detection
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
            teacher.GetComponent<AudioSource>().PlayOneShot(no);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            TrainingManager.trainingNumber -= 1;

            StartCoroutine(teacher.GetComponentInChildren<TrainingTrigger>().ResetTrainer());
            player.GetComponent<LocomotionSwitch>().locomotionOn = true;
        }

        IEnumerator Outro()
        {
            StartCoroutine(FloatSphereToPlayer());
            
            teacher.GetComponent<AudioSource>().PlayOneShot(outro);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            Destroy(teacher);
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
