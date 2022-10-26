using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GoalTrigger : MonoBehaviour
{
    public PortalSceneChange sceneChange;
    public PlayerStatManager levelProg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            levelProg.levelProgression += 1;
            sceneChange.OpenPortal(1);
        }
    }
}
