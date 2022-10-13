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

    public void StartNewTurn(Combatant actionTaker, Combatant actionTarget)
    {
        currentAction = new BattleAction();
        currentAction.actionTaker = actionTaker;
        currentAction.actionTarget = actionTarget;
        DetermineAction();
    }

    private void DetermineAction()
    {
        currentAction.actionTaker.ActionSelected.AddListener(ActionSelected);
        currentAction.actionTaker.SelectAction();
    }

    private void ActionSelected(BattleAttackAbility ability)
    {
        currentAction.selectedAbility = ability;
        currentAction.actionTarget.DefenseSelected.AddListener(DefenseSelected);
        currentAction.actionTarget.SelectDefense();
    }

    private void DefenseSelected(DefenseAbility ability)
    {
        currentAction.selectedDefense = ability;
        SynchronizeStart();
    }

    private void SynchronizeStart()
    {
        currentAction.actionTaker.TurnActionComplete.AddListener(ActionComplete);
        currentAction.actionTaker.TakeAction();
        currentAction.selectedDefense.AbilityComplete.AddListener(DefenseComplete);
        currentAction.actionTarget.TakeDefense(currentAction.selectedAbility.abilityDuration);
    }

    private void ActionComplete(int attackPower)
    {
        currentAction.attackPower = attackPower;
        currentAction.attackComplete = true;
        ActionAndDefenseComplete();

    }

    private void DefenseComplete(int defensePower)
    {
        currentAction.defensePower = defensePower;
        currentAction.defenseComplete = true;
        ActionAndDefenseComplete();
    }

    private void ActionAndDefenseComplete()
    {
        if (currentAction.attackComplete && currentAction.defenseComplete)
        {
            currentAction.actionTaker.ActionSelected.RemoveListener(ActionSelected);
            currentAction.actionTaker.TurnActionComplete.RemoveListener(ActionComplete);
            currentAction.actionTarget.DefenseSelected.RemoveListener(DefenseSelected);
            currentAction.selectedDefense.AbilityComplete.RemoveListener(DefenseComplete);
            TurnComplete.Invoke(currentAction);
            ResetForNextTurn();
        }
    }

    private void ResetForNextTurn()
    {
       // currentAction = null;
    }
}
