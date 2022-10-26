using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentFist : MonoBehaviour
{
    [SerializeField] private HandAnimationController hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hand.Fist(true);
    }
}
