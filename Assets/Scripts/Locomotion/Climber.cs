using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class Climber : MonoBehaviour
{

    private CharacterController character;
    private Vector3 velocity;
    
    public static ActionBasedController climbingHand;
    public InputActionProperty velocityProp;
    private ContinuousMoveProviderBase _continuousMoveProviderBase;
    private ElectricOwl _electricOwl;
    
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        character = transform.parent.parent.GetComponent<CharacterController>();
        _continuousMoveProviderBase = transform.parent.parent.GetComponent<ContinuousMoveProviderBase>();
        //_electricOwl = GetComponent<ElectricOwl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbingHand)
        {
            _continuousMoveProviderBase.enabled = false;
            Climb();
        }
        else
        {
            _continuousMoveProviderBase.enabled = true;

        }
    }

    private void Climb()
    {
        Debug.Log("Start Climbing");
        if(climbingHand == transform.GetComponent<ActionBasedController>())
        velocity = velocityProp.action.ReadValue<Vector3>();
        //InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime );
    }
}
