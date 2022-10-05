using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combatant : MonoBehaviour
{
    

    public UnityEvent<BattleAbility> ActionSelected = new UnityEvent<BattleAbility>();
    public UnityEvent DefenseSelected = new UnityEvent();
    public UnityEvent<int> TurnActionComplete = new UnityEvent<int>();

    public BattleAbility[] actionAbilities;
    protected BattleAbility selectedAbility;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SelectAction()
    {
        actionAbilities = GetComponents<BattleAbility>();
        selectedAbility = actionAbilities[0];
        ActionSelected.Invoke(actionAbilities[0]);
    }

    public void TakeAction()
    {
        selectedAbility.AbilityComplete.AddListener(CompleteTurnAction);
        selectedAbility.ExecuteAction();
    }

    private void CompleteTurnAction(int attackPower)
    {
        ResetForNextTurn();
        TurnActionComplete.Invoke(attackPower);
        
    }

    protected virtual void ResetForNextTurn()
    {
        selectedAbility.AbilityComplete.RemoveListener(CompleteTurnAction);
    }
    
}
