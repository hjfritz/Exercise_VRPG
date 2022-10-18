using System.Collections;
using System.Net;
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
        
        //AudioClips
        [SerializeField] private AudioClip intro2;
        [SerializeField] private AudioClip no2;
        [SerializeField] private AudioClip training3;
        [SerializeField] private AudioClip outroA3;

        public static bool option = false;
        public static bool done = false;

        private GameObject teacher;
        private bool chosen = false;
        private Animator _animator;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (chosen)
            {
                if (option)
                {
                    StartCoroutine(YesOption());
                }
                else
                {
                    StartCoroutine(NoOption());
                }

                chosen = false;
            }

            if (done)
            {
                Outro();
                done = false;
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
            
            //Insert Button Options
            
            //For Testing
            StartCoroutine(YesOption());
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
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            _animator.SetBool("ForceFieldTraining", false);
            
            //Insert Squat ability detection
            
            //For Testing
            StartCoroutine(Outro());
        }
        
        IEnumerator NoOption()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(no2);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            TrainingManager.trainingNumber -= 1;
        }

        IEnumerator Outro()
        {
            Instantiate(orbA1, teacher.transform.position, quaternion.identity);
            
            teacher.GetComponent<AudioSource>().PlayOneShot(outroA3);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            TrainingManager.currentTeacher.SetActive(false);
            player.GetComponent<LocomotionSwitch>().locomotionOn = true;
        }
    }
}
