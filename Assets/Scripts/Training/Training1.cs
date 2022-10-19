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
        
        [SerializeField] private GameObject orbA1;
        [SerializeField] private GameObject player;
        
        //AudioClips
        [SerializeField] private AudioClip intro;
        [SerializeField] private AudioClip yes;
        [SerializeField] private AudioClip no;
        [SerializeField] private AudioClip training;
        [SerializeField] private AudioClip outro;

        private GameObject teacher;
        private bool chosen = false;
        private Animator _animator;
        private bool actionComplete = false;

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
            StartCoroutine(Outro());
        }

        // IEnumerator DetectAction()
        // {
        //     
        // }
        
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
            Instantiate(orbA1, teacher.transform.position, quaternion.identity);
            
            teacher.GetComponent<AudioSource>().PlayOneShot(outro);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            Destroy(teacher);
            player.GetComponent<LocomotionSwitch>().locomotionOn = true;
            
        }
    }
}
