using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;
    
    public XROrigin player;
    public PlayerStatManager level;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerStatManager>().Load();
        if (level.levelProgression > 1)
        {
            player.transform.position = returnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
