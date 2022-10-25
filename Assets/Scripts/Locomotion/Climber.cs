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
    [SerializeField] private CapsuleCollider col;
    [SerializeField] private Rigidbody rb;

    public static XRBaseController climbingHand;

    private ContinuousMoveProviderBase continuousMovement;
    
    /* seemingly no need to disable electric owl while climbing.
       unlikely player would grip and press primary buttons at the same time
       seems like a livable bug for MVP*/
    //private ElectricOwl _electricOwl;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMoveProviderBase>();
        //_electricOwl = GetComponent<ElectricOwl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbingHand)
        {
            continuousMovement.enabled = false;
            character.enabled = true;
            col.enabled = false;
            rb.isKinematic = true;
            
            //_electricOwl.enabled = false;
            Climb();
        }
        else
        {
            continuousMovement.enabled = true;
            character.enabled = false;
            col.enabled = true;
            rb.isKinematic = false;
            //_electricOwl.enabled = true;

        }
    }

    private void Climb()
    {
        Debug.Log("Start Climbing");
        Debug.Log(climbingHand.name);
        
        XRNode node = climbingHand.name.Contains("Left")? XRNode.LeftHand : XRNode.RightHand;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move( transform.rotation * -velocity * Time.fixedDeltaTime );
    }
}
