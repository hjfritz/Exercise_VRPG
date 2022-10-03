using UnityEditor.XR.Interaction.Toolkit.AR;
using UnityEngine;

namespace ForceField
{
    public class CircleTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject Diameter;
        [SerializeField] private Transform Hand;

        private Vector3 Hand2D;
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Hand2D = new Vector3(Hand.position.x, Hand.position.y, Diameter.transform.position.z);
            
            Diameter.transform.LookAt(Hand2D);
        }
    }
}
