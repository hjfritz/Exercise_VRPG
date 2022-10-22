using System.Collections;
using System.Collections.Generic;
using Training;
using UnityEngine;

public class CombatTwirler : MonoBehaviour
{

    private PlayerManager _pm;
    // Start is called before the first frame update
    void Start()
    {
        _pm = FindObjectOfType<PlayerManager>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_pm.currentCombatManager)
        {
            this.transform.LookAt(_pm.currentCombatManager.transform);
        }
        else if (TrainingManager.currentTeacher)
        {
            this.transform.LookAt(TrainingManager.currentTeacher.transform);
        }
    }
}
