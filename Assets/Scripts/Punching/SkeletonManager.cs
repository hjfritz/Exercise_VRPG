using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int _resistance;
    [SerializeField] private int _hitstaken;
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private Animator _animator;
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
            _animator.SetBool("Dead",true);
        }
        else
        {
            _animator.SetBool("Attack",true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        PunchAbility punchAbility= other.GetComponent<PunchAbility>();
        if (punchAbility)
        {
            punchAbility.Hit();
            punchSound.Play();
        }
    }


}
