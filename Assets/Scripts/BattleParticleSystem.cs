using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem battle;

    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PSON()
    {
        battle.Play();
    }

    public void PSOff()
    {
        battle.Stop();
    }
}
