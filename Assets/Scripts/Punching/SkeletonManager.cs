using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void Attack()
    {

        _animator.SetBool("Attack",true);
    }

    public void Death()   
    {
        _animator.SetBool("Attack",false);
        _animator.SetBool("Dead",true);
    }
    
    public void Defense()   
    {
        _animator.SetBool("Attack",false);
    }
    

}
