using UnityEngine;

namespace Training
{
    public class TrainerActive : MonoBehaviour
    {
        public PlayerStatManager level;

        [SerializeField] private GameObject trainers;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (level.levelProgression > 1)
            {
                trainers.SetActive(false);
            }
        }
    }
}
