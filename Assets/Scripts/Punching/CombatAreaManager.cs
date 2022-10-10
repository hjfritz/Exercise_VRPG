using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatAreaManager : MonoBehaviour
{
    public UnityEvent StartFight;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        StartFight.Invoke();
    }
}
