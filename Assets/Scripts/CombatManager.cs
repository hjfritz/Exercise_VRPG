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
    
    // Start is called before the first frame update
    void Start()
    {
        BattleTurnOrder = new Combatant[PartyMembers.Length + EnemyPartyMembers.Length];
        int i = 0;
        for (; i < PartyMembers.Length; i++)
        {
            BattleTurnOrder[i] = PartyMembers[i];
        }

        for (int j = 0; j < EnemyPartyMembers.Length; j++)
        {
            BattleTurnOrder[i] = EnemyPartyMembers[j];
            i++;
        }
        
        seq = new CombatTurnSequencer();
        seq.TurnComplete.AddListener(ResolveTurn);
        NextTurn();
    }

    private void ResolveTurn(BattleAction battleAction)
    {
        Debug.Log($"Action Taker - {battleAction.actionTaker}, Attack Ability - {battleAction.selectedAbility}, Attack Power - {battleAction.attackPower}");
        

        int damage = Mathf.FloorToInt(.2f * battleAction.attackPower);

        var damageTarget = BattleTurnOrder[(turnCounter + 1) % BattleTurnOrder.Length];
        damageTarget.HP -= damage;
        Debug.Log($"Damage - {damage} to {damageTarget}");

        if (damageTarget.HP <= 0)
        {
            Debug.Log($"Battle Over - Fatal Blow Dealt by {battleAction.actionTaker}");
        }
        else
        {
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
        seq.StartNewTurn(BattleTurnOrder[turnCounter%BattleTurnOrder.Length]);
    }
}
