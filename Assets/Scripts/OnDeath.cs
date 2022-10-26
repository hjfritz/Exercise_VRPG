using System.Collections;
using System.Collections.Generic;
using Training;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private Transform transportPoint;
    [SerializeField] private GameObject playerRig;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip deathMusic;

    public void GoToPurgatory()
    {
        playerRig.transform.position = transportPoint.position;
        _audioSource.Stop();
        _audioSource.PlayOneShot(deathMusic);
    }
}
