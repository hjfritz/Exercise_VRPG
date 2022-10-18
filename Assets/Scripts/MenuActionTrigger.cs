using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuActionTrigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public void OnTriggerEnter(Collider other)
    {
        OnEnter.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
