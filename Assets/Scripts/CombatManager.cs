using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Combatant[] PartyMembers;
    [SerializeField] private Combatant[] EnemyPartyMembers;
    
    [SerializeField] private Combatant[] BattleTurnOrder;
    private CombatTurnSequencer seq;
    
    private int turnCounter = 0;

    [SerializeField] private CombatManagerMenu menu;
    
    // Start is called before the first frame update
    void Start()
    {
        menu.ShowWelcomeCanvas();
    }

    public void StartBattle()
    {
        menu.ShowBattleHudCanvas();
        BattleTurnOrder = new Combatant[PartyMembers.Length + EnemyPartyMembers.Length];
        int i = 0;
        for (; i < PartyMembers.Length; i++)
        {
            BattleTurnOrder[i] = PartyMembers[i];
            PartyMembers[i].UpdateHP(100);
        }

        for (int j = 0; j < EnemyPartyMembers.Length; j++)
        {
            BattleTurnOrder[i] = EnemyPartyMembers[j];
            EnemyPartyMembers[j].UpdateHP(100);
            i++;
        }

        turnCounter = 0;
        seq = new CombatTurnSequencer();
        seq.TurnComplete.AddListener(ResolveTurn);
        NextTurn();
        
    }

    private void ResolveTurn(BattleAction battleAction)
    {
        Debug.Log($"Action Taker - {battleAction.actionTaker}, Attack Ability - {battleAction.selectedAbility}, Attack Power - {battleAction.attackPower}");
        

        int damage = Mathf.FloorToInt(.2f * battleAction.attackPower);

        
        var damageTarget = BattleTurnOrder[(turnCounter + 1) % BattleTurnOrder.Length];
        var damageTargetHP = damageTarget.GetHP();
        var damageTargetNewHP = damageTargetHP - damage;
        
        Debug.Log($"Damage - {damage} to {damageTarget}");

        if (damageTargetNewHP <= 0)
        {
            Debug.Log($"Battle Over - Fatal Blow Dealt by {battleAction.actionTaker}");
            damageTarget.UpdateHP(0);
            menu.ShowRestartCanvas();
        }
        else
        {
            damageTarget.UpdateHP(damageTargetNewHP);
            turnCounter++;
            NextTurn();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextTurn()
    {
        menu.turnIndicator.ShowTurnTaker(BattleTurnOrder[turnCounter%BattleTurnOrder.Length].name);
        seq.StartNewTurn(BattleTurnOrder[turnCounter%BattleTurnOrder.Length]);
    }
}
