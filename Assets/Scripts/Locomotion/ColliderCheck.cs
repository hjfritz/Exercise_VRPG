using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"I have a rigidBody {GetComponent<Rigidbody>()}");
        Debug.Log($"I have a collider =  {GetComponent<Collider>()}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} triggered by {other.gameObject.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.collider.gameObject.name}");
        Debug.Log($"{collision.collider.gameObject.name} isKinematic = {collision.collider.GetComponent<Rigidbody>().isKinematic}");
        Debug.Log(GetComponent<Rigidbody>().isKinematic);
    }
}
