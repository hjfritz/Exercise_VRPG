using UnityEngine;

namespace Locomotion
{
    public class PlayerCentering : MonoBehaviour
    {
        [SerializeField] private GameObject centerDot;
        [SerializeField] private Transform headPos;
        [SerializeField] private Transform rigCenter;
        [SerializeField] private float margin = .1f;

        private Vector3 adjustedHeadPos;
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            adjustedHeadPos = new Vector3(headPos.position.x, rigCenter.position.y, headPos.position.z);
            
            if (Vector3.Distance(adjustedHeadPos, rigCenter.position) > margin)
            {
                centerDot.SetActive(true);
            }
            else
            {
                centerDot.SetActive(false);
            }
        }
    }
}
