using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

public class Climber : MonoBehaviour
{

    private CharacterController character;

    public static XRBaseController climbingHand;

    private ContinuousMoveProviderBase continuousMovement;

    //private ElectricOwl _electricOwl;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMoveProviderBase>();
        //_electricOwl = transform.parent.parent.GetComponent<ElectricOwl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbingHand)
        {
            continuousMovement.enabled = false;
            //_electricOwl.enabled = false;
            Climb();
        }
        else
        {
            continuousMovement.enabled = true;
            //_electricOwl.enabled = true;

        }
    }

    private void Climb()
    {
        Debug.Log("Start Climbing");
        //if(climbingHand == transform.GetComponent<ActionBasedController>())
        //velocity = velocityProp.action.ReadValue<Vector3>();
        Debug.Log(climbingHand.name);
        
        XRNode node = climbingHand.name.Contains("Left")? XRNode.LeftHand : XRNode.RightHand;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move( transform.rotation * -velocity * Time.fixedDeltaTime );
    }
}
