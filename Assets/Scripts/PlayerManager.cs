using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    
    [Range(1,2)] public int difficulty;
    public CombatManager currentCombatManager;
    public GameObject menu;

    public UnityEvent FightStart;
    public UnityEvent FightEnd;

    public int MAX_DIFFICULTY = 2;
    public void SetDifficulty(int diff)
    {
        difficulty = diff;

    }
}
