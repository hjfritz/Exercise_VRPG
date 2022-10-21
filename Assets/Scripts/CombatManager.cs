using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Training;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Combatant[] PartyMembers;
    [SerializeField] private Combatant[] EnemyPartyMembers;

    [SerializeField] private AudioSource BGM;
    
    [SerializeField] private Combatant[] BattleTurnOrder;
    private CombatTurnSequencer seq;
    
    private int turnCounter = 0;

    private CombatManagerMenu menu;

    public bool battleActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        PartyMembers = FindObjectsOfType<PlayerCombatant>(true);
        menu = FindObjectOfType<PlayerManager>(true).GetComponentInChildren<CombatManagerMenu>();
        menu.HideMenu();
        //check to see if it is the training island or the level1 scene.
        if (!GetComponent<TrainingTrigger>())
        {
            gameObject.SetActive(false);
        }
        
    }

    public void StartBattle()
    {
        if (battleActive == false)
        {
            menu.ShowBattleHudCanvas();
            battleActive = true;
        
            //BGM.Play();
        
            BattleTurnOrder = new Combatant[PartyMembers.Length + EnemyPartyMembers.Length];
            int i = 0;
            for (; i < PartyMembers.Length; i++)
            {
                BattleTurnOrder[i] = PartyMembers[i];
                PartyMembers[i].healthBar.gameObject.SetActive(true);
                PartyMembers[i].HealToFullHealth();
            }

            for (int j = 0; j < EnemyPartyMembers.Length; j++)
            {
                BattleTurnOrder[i] = EnemyPartyMembers[j];
                EnemyPartyMembers[j].healthBar.gameObject.SetActive(true);
                EnemyPartyMembers[j].UpdateHP(EnemyPartyMembers[j].GetHP());
                EnemyPartyMembers[j].HealToFullHealth();
                i++;
            }

            turnCounter = 0;
            seq = new CombatTurnSequencer();
            seq.TurnComplete.AddListener(ResolveTurn);
            NextTurn();
        }
        
    }

    private void ResolveTurn(BattleAction battleAction)
    {
        Debug.Log($"Action Taker - {battleAction.actionTaker}, Attack Ability - {battleAction.selectedAbility}");
        Debug.Log($"Action Target - {battleAction.actionTarget}");
        
        
        if (battleAction.actionTarget.GetHP() <= 0)
        {
            Debug.Log($"Battle Over - Fatal Blow Dealt by {battleAction.actionTaker}");
            menu.HideMenu();
            //BGM.Stop();
            ShowHideHealthBars(false);
            battleActive = false;

            // ending the battle and deactivating the Combat Area Manager
            CombatAreaManager CAM = FindObjectOfType<CombatAreaManager>();
            if (CAM)
            {
                CAM.endoffight();
            }
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
        menu.turnIndicator.ShowTurnTaker(BattleTurnOrder[turnCounter%BattleTurnOrder.Length].displayName);
        seq.StartNewTurn(BattleTurnOrder[turnCounter%BattleTurnOrder.Length],BattleTurnOrder[(turnCounter+1)%BattleTurnOrder.Length] );
    }

    private void ShowHideHealthBars(bool show)
    {
        foreach (var combatant in BattleTurnOrder)
        {
            combatant.healthBar.gameObject.SetActive(show);
        }
    }
}
