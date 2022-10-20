using UnityEngine;

namespace Training
{
    public class ApplyGravity : MonoBehaviour
    {
        private bool _gravity = false;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (_gravity)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        public void GravSwitch()
        {
            _gravity = true;
        }
    }
}
