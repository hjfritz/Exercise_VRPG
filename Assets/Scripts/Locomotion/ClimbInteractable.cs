using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Locomotion
{
    public class ClimbInteractable : XRBaseInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            
            XRDirectInteractor xrd = args.interactorObject.transform.GetComponent<XRDirectInteractor>();
            if (xrd)
            {
                Climber.climbingHand = args.interactorObject.transform
                    .GetComponent<ActionBasedController>();
            }
                
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            
            XRDirectInteractor xrd = args.interactorObject.transform.GetComponent<XRDirectInteractor>();
            if(xrd)
            {
                if(Climber.climbingHand && Climber.climbingHand.name == args.interactorObject.transform.name)
                    Climber.climbingHand = null;
            }
            
        }
    }
}
