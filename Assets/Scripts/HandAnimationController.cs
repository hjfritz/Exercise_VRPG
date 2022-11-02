using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionReference grab;
    [SerializeField] private SkinnedMeshRenderer renderer;

    private void Start()
    {
        grab.action.performed += MakeFist;
        grab.action.canceled += ReleaseFist;
        
    }

    private void ReleaseFist(InputAction.CallbackContext obj)
    {
        Fist(false);
    }

    private void MakeFist(InputAction.CallbackContext obj)
    {
        Fist(true);
    }
    
    

    public void Fist(bool fist)
    {
        animator.SetBool("Fist", fist);
    }

    public void HideHand(bool hide)
    {
        renderer.enabled = !hide;
    }
}
