using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    [SerializeField] private int _resistance;
    [SerializeField] private int _hitstaken;
    [SerializeField] private AudioSource punchSound;

    [SerializeField] private GameObject brokenBoulder;
    // Start is called before the first frame update
    void Start()
    {
        _hitstaken = 0;
    }
   

    public void hittouch(int damage)
    {
        _hitstaken += damage;
        if (_hitstaken >= _resistance)
        {
            breakdown();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        PunchAbility punchAbility= other.GetComponent<PunchAbility>();
        if (punchAbility)
        {
            //punchAbility.Hit();
            punchSound.Play();
        }
    }

    private void breakdown()
    {
        var gm = Instantiate(brokenBoulder);
        /*gm.GetComponent<RockManager>()._hitstaken = 0;
        gm.transform.localScale = transform.localScale / 2;
        gm = Instantiate(gameObject);
        gm.GetComponent<RockManager>()._hitstaken = 0;
        gm.transform.localScale = transform.localScale / 2;*/
        Destroy(gameObject);
    }
}
