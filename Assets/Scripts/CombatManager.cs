using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Combatant[] PartyMembers;
    private CombatTurnSequencer seq;
    // Start is called before the first frame update
    void Start()
    {
        seq = new CombatTurnSequencer();
        seq.TurnComplete.AddListener(ResolveTurn);
        NextTurn();
    }

    private void ResolveTurn(BattleAction battleAction)
    {
        //Debug.Log($"Action Taker - {battleAction.actionTaker}");
        Debug.Log($"Attack Power - {battleAction.attackPower}");
        
        NextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextTurn()
    {
        seq.StartNewTurn(PartyMembers[0]);
    }
}
