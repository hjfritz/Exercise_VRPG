using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    // bug fix- this was defaulting to 0 (despite this range attribute), ememies were dying after one hit
    // explicitly setting to default to 1
    [Range(1,5)] public int difficulty = 1;
    public CombatManager currentCombatManager;
    public GameObject menu;

    public UnityEvent FightStart;
    public UnityEvent FightEnd;

    public void SetDifficulty(int diff)
    {
        difficulty = diff;

    }
}
