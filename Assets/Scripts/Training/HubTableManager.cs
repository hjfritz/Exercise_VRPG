using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Training
{
    public class HubTableManager : MonoBehaviour
    {
        [SerializeField] private GameObject socketParent;
        [SerializeField] private GameObject orbParent;
        
        [SerializeField] private XRSocketInteractor socket1;
        [SerializeField] private XRSocketInteractor socket2;
        [SerializeField] private XRSocketInteractor socket3;
        [SerializeField] private XRSocketInteractor socket4;
        [SerializeField] private XRSocketInteractor socket5;

        [SerializeField] private GameObject map;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        [SerializeField] private AudioSource AS;
        [SerializeField] private AudioClip grind;

        [SerializeField] private int SphereCount = 0;

        public PlayerStatManager trainersDone;
        
        // Start is called before the first frame update
        void Start()
        {
            SphereCount = trainersDone.trainersFinished;

            if (SphereCount < 5)
            {
                socket1.selectEntered.AddListener(SphereIn);
                socket2.selectEntered.AddListener(SphereIn);
                socket3.selectEntered.AddListener(SphereIn);
                socket4.selectEntered.AddListener(SphereIn);
                socket5.selectEntered.AddListener(SphereIn);
            
                socket1.selectExited.AddListener(SphereOut);
                socket2.selectExited.AddListener(SphereOut);
                socket3.selectExited.AddListener(SphereOut);
                socket4.selectExited.AddListener(SphereOut);
                socket5.selectExited.AddListener(SphereOut);

                map.transform.position = start.position;
            }
        }

        private void SphereOut(SelectExitEventArgs arg0)
        {
            SphereCount--;
            trainersDone.trainersFinished--;
        }

        private void SphereIn(SelectEnterEventArgs arg0)
        {
            SphereCount++;
            trainersDone.trainersFinished++;
        }

        // Update is called once per frame
        void Update()
        {
            if (SphereCount == 5)
            {
                //Lift Map
                if (map.transform.localPosition.z < end.localPosition.z)
                {
                    AS.PlayOneShot(grind);
                    map.transform.Translate(Vector3.up * Time.deltaTime * .1f);
                }
                else
                {
                    AS.Stop();
                    socketParent.SetActive(false);
                    orbParent.SetActive(false);
                    MapManager.map = true;
                    SphereCount++;
                    trainersDone.trainersFinished = 5;
                }
            }

            if (SphereCount > 5)
            {
                MapManager.map = true;
                map.transform.position = end.position;
            }
        }
    }
}
