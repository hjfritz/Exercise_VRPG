using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    [SerializeField] private int _resistance;
    [SerializeField] private int _hitstaken;
    // Start is called before the first frame update
    void Start()
    {
        _hitstaken = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            Debug.Log("Triggered Entered");
            _hitstaken++;
            if (_hitstaken >= _resistance)
            {
                breakdown();
            }
        }

    }

    private void breakdown()
    {
        var gm = Instantiate(gameObject);
        gm.GetComponent<RockManager>()._hitstaken = 0;
        gm.transform.localScale = transform.localScale / 2;
        gm = Instantiate(gameObject);
        gm.GetComponent<RockManager>()._hitstaken = 0;
        gm.transform.localScale = transform.localScale / 2;
        Destroy(gameObject);
    }
}
