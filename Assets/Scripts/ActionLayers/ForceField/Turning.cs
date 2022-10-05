using UnityEngine;

namespace ActionLayers.ForceField
{
    public class Turning : MonoBehaviour
    {
        [SerializeField] private bool forward = true;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (forward)
            {
                transform.Rotate(0, 1, 0);
            }
            else
            {
                transform.Rotate(0, .5f, 0);
            }
        }
    }
}
