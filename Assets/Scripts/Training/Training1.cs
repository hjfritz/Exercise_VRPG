using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

namespace Training
{
    public class Training1 : MonoBehaviour
    {
        [SerializeField] private GameObject teacher;
        [SerializeField] private GameObject orbA1;
        
        //AudioClips
        [SerializeField] private AudioClip intro;
        [SerializeField] private AudioClip yes;
        [SerializeField] private AudioClip no;
        [SerializeField] private AudioClip training;
        [SerializeField] private AudioClip outro;

        public static bool option = false;
        public static bool punched = false;

        private bool chosen = false;
        
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
                    YesOption();
                }
                else
                {
                    NoOption();
                }

                chosen = false;
            }

            if (punched)
            {
                Outro();
                punched = false;
            }
        }

        public void StartTraining1()
        {
            StartCoroutine(PlayIntro());
        }

        IEnumerator PlayIntro()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(intro);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            Debug.Log("played");
            
            //Insert Button Options
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
            teacher.GetComponent<AudioSource>().PlayOneShot(yes);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            teacher.GetComponent<AudioSource>().PlayOneShot(training);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
            
            //Insert Squat ability detection
        }
        
        IEnumerator NoOption()
        {
            teacher.GetComponent<AudioSource>().PlayOneShot(no);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
        }

        IEnumerator Outro()
        {
            Instantiate(orbA1);
            
            teacher.GetComponent<AudioSource>().PlayOneShot(outro);
            yield return new WaitUntil((() => teacher.GetComponent<AudioSource>().isPlaying == false));
        }
    }
}
