using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Locomotion
{
    public class ClimbInteractable : XRBaseInteractable
    {
        protected override void OnSelectEntered(XRBaseInteractor interactor)
        {
            base.OnSelectEntered(interactor);
            Debug.Log("climb interactable select enter");

            if (interactor is XRDirectInteractor)
            {
                Climber.climbingHand = interactor.GetComponent<XRBaseController>();
            }
        }

        protected override void OnSelectExited(XRBaseInteractor interactor)
        {
            base.OnSelectExited(interactor);
            Debug.Log("climb interactable select exit");
            
           if(interactor is XRDirectInteractor)
            {
                if(Climber.climbingHand && Climber.climbingHand.name == interactor.name)
                    Climber.climbingHand = null;
            }
        }
        
    }
}
