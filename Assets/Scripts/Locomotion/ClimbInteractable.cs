using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("Start CLimbing");
        if(args.interactorObject is XRDirectInteractor)
        Climber.climbingHand = args.interactorObject.transform.GetComponent<XRController>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log("Stop CLimbing");
        if (args.interactorObject is XRDirectInteractor)
        {
            if(Climber.climbingHand && Climber.climbingHand.name == args.interactorObject.transform.name)
                Climber.climbingHand = null;
        }
            
    }
}
