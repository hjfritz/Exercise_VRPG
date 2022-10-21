using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Collectible : MonoBehaviour
{
    private enum Effect
    {
        Potion,
        Gloves,
        Coin
    }
    private GameObject gm;
    private PlayerStatManager pmsm;
    private AudioSource audio;
    [SerializeField]private Effect effect;

    [SerializeField] private int collectibleValue;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        pmsm = gm.GetComponent<PlayerStatManager>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            audio.Play();
            if (effect == Effect.Potion)
            {
                pmsm.hps += collectibleValue;
            }
            else if (effect == Effect.Coin)
            {
                pmsm.countCoins += collectibleValue;
            }
            else if (effect == Effect.Gloves)
            {
                pmsm.hasGloves = true;
            }
            
            Destroy(gameObject,1);
        }
    }
}
