using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticInteractable : MonoBehaviour
{

    [Range(0, 1)] public float intensity;

    public float duration;

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
            controller.SendHapticImpulse(intensity, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        var controllerInteractor = other.gameObject.GetComponent<XRBaseControllerInteractor>();
        if (controllerInteractor)
        {
            Debug.Log("Trigger Haptic");
            
            TriggerHaptic(controllerInteractor.xrController);
        }

        var rig = other.gameObject.GetComponent<XROrigin>();
        if (rig)
        {
            var controllers = rig.GetComponentsInChildren<XRBaseControllerInteractor>();
            foreach (var controller in controllers)
            {
                TriggerHaptic(controller.xrController);
            }
        }
    }
}
