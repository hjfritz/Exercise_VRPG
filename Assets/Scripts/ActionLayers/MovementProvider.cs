using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ActionLayers
{
    public class MovementProvider : LocomotionProvider
    {
        [SerializeField] private GameObject actionLayers;
        
        public float speed = 1.0f;
        public float gravityMultiplier = 1.0f;
        public List<XRController> controllers = null;

        private CharacterController _characterController = null;
        private Camera head = null;

        protected override void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            head = GetComponent<XROrigin>().Camera;
        }

        // Start is called before the first frame update
        private void Start()
        {
            PositionController();
        }

        // Update is called once per frame
        void Update()
        {
            PositionController();
            CheckForInput();
            ApplyGravity();
        }

        private void CheckForInput()
        {
            foreach (XRController controller in controllers)
            {
                if (controller.enableInputActions)
                    CheckForMovement(controller.inputDevice);
            }
        }

        private void CheckForMovement(InputDevice controllerInputDevice)
        {
            if (controllerInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
                StartMove(position);
        }

        private void StartMove(Vector2 position)
        {
            Vector3 direction = new Vector3(position.x, 0, position.y);
            Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

            direction = Quaternion.Euler(headRotation) * direction;

            Vector3 movement = direction * speed;
            _characterController.Move(movement * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
            gravity.y *= Time.deltaTime;

            _characterController.Move(gravity * Time.deltaTime);
        }

        private void PositionController()
        {
            float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
            _characterController.height = headHeight;
        
            Vector3 newCenter = Vector3.zero;
            newCenter.y = _characterController.height / 2;
            newCenter.y += _characterController.skinWidth;

            newCenter.x = head.transform.localPosition.x;
            newCenter.z = head.transform.localPosition.z;

            _characterController.center = newCenter;
        }
    }
}
