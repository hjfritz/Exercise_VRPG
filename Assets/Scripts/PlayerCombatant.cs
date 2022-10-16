using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : Combatant
{
    [SerializeField] private BattleActionMenu actionMenu;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SelectAction()
    {
        actionAbilities = GetComponents<BattleAttackAbility>();
        actionMenu.MenuActionSelected.AddListener(MenuActionSelected);
        displayActionMenu();
    }
    
    private void MenuActionSelected(int arg0)
    {
        selectedAbility = actionAbilities[arg0];
        ActionSelected.Invoke(actionAbilities[arg0]);
        hideActionMenu();
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
    
    protected override void ResetForNextTurn()
    {
        base.ResetForNextTurn();
        actionMenu.MenuActionSelected.RemoveListener(MenuActionSelected);
    }
}
