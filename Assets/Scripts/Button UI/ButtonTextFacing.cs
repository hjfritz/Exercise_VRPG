using UnityEngine;

namespace Button_UI
{
    public class ButtonTextFacing : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
                
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(_camera.transform);
        }
    }
}
