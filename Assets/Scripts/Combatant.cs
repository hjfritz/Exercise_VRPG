using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combatant : MonoBehaviour
{
    [SerializeField] private BattleActionMenu actionMenu;

    public UnityEvent<BattleAbility> ActionSelected = new UnityEvent<BattleAbility>();
    public UnityEvent DefenseSelected = new UnityEvent();
    public UnityEvent<int> TurnActionComplete = new UnityEvent<int>();

    public BattleAbility[] actionAbilities;
    private BattleAbility selectedAbility;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAction()
    {
        actionAbilities = GetComponents<BattleAbility>();
        actionMenu.MenuActionSelected.AddListener(MenuActionSelected);
        displayActionMenu();
        
    }

    private void MenuActionSelected(int arg0)
    {
        selectedAbility = actionAbilities[arg0];
        ActionSelected.Invoke(actionAbilities[arg0]);
        hideActionMenu();
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

    public void displayActionMenu()
    {
        actionMenu.BuildMenu(actionAbilities);
        actionMenu.gameObject.SetActive(true);
    }

    public void hideActionMenu()
    {
        actionMenu.gameObject.SetActive(false);
    }

    private void ResetForNextTurn()
    {
        selectedAbility.AbilityComplete.RemoveListener(CompleteTurnAction);
        actionMenu.MenuActionSelected.RemoveListener(MenuActionSelected);
    }
    
}
