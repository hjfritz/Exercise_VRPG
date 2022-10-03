using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatTurnSequencer
{
    //private Combatant actionTaker;
    //private Combatant actionTarget;
    private BattleAction currentAction;
    public UnityEvent<BattleAction> TurnComplete = new UnityEvent<BattleAction>();

    public void StartNewTurn(Combatant actionTaker)
    {
        currentAction = new BattleAction();
        currentAction.actionTaker = actionTaker;
        DetermineAction();
    }

    private void DetermineAction()
    {
        currentAction.actionTaker.ActionSelected.AddListener(ActionSelected);
        currentAction.actionTaker.SelectAction();
    }

    private void ActionSelected(BattleAbility ability)
    {
        currentAction.selectedAbility = ability;
        currentAction.actionTaker.TurnActionComplete.AddListener(ActionComplete);
        currentAction.actionTaker.TakeAction();
    }

    private void ActionComplete(int attackPower)
    {
        currentAction.attackPower = attackPower;
        currentAction.actionTaker.ActionSelected.RemoveListener(ActionSelected);
        currentAction.actionTaker.TurnActionComplete.RemoveListener(ActionComplete);
        TurnComplete.Invoke(currentAction);
        ResetForNextTurn();
    }

    private void ResetForNextTurn()
    {
       // currentAction = null;
    }
}
