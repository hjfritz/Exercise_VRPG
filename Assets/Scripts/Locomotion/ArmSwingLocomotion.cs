using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArmSwingLocomotion : MonoBehaviour
{

    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject CenterEyeCamera;
    public GameObject ForwardDirection;

    private Vector3 PositionPreviousFrameLeftHand;
    private Vector3 PositionPreviousFrameRightHand;
    private Vector3 PositionPreviousFramePlayer;
    
    private Vector3 PositionThisFrameLeftHand;
    private Vector3 PositionThisFrameRightHand;
    private Vector3 PositionThisFramePlayer;

    public float Speed = 70;
    private float HandSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PositionPreviousFramePlayer = transform.position;
        PositionPreviousFrameLeftHand = LeftHand.transform.position;
        PositionThisFrameRightHand = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = CenterEyeCamera.transform.eulerAngles.y;
        ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        PositionThisFrameLeftHand = LeftHand.transform.position;
        PositionThisFrameRightHand = RightHand.transform.position;
        PositionThisFramePlayer = transform.position;

        var playerDistanceMoved = Vector3.Distance(PositionThisFramePlayer, PositionPreviousFramePlayer);
        var leftHandDistanceMoved = Vector3.Distance(PositionThisFrameLeftHand, PositionPreviousFrameLeftHand);
        var rightHandeDistanceMoved = Vector3.Distance(PositionThisFrameRightHand, PositionPreviousFrameRightHand);

        HandSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandeDistanceMoved - playerDistanceMoved));

        if (Time.timeSinceLevelLoad > 1f)
        {
            transform.position += ForwardDirection.transform.forward * HandSpeed * Speed * Time.deltaTime;
            
        }

        PositionPreviousFrameLeftHand = PositionThisFrameLeftHand;
        PositionPreviousFrameRightHand = PositionThisFrameRightHand;
        PositionPreviousFramePlayer = PositionThisFramePlayer;
    }
}
