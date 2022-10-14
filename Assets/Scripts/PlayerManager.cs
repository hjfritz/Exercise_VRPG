using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    
    [SerializeField][Range(0,10)] private int difficulty;
    public CombatManager currentCombatManager;
    public GameObject menu;

}
