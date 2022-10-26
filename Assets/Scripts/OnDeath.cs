using System.Collections;
using System.Collections.Generic;
using Training;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private Transform transportPoint;
    [SerializeField] private GameObject playerRig;

    public void GoToPurgatory()
    {
        playerRig.transform.position = transportPoint.position;
    }
}
