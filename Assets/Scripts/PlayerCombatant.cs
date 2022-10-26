using System.Collections;
using System.Collections.Generic;
using ActionLayers.EnergyBallAttack;
using UnityEngine;

public class PlayerCombatant : Combatant
{
    [SerializeField] private BattleActionMenu actionMenu;
    [SerializeField] private Kenobi kenobi;
    
    [SerializeField] private HandAnimationController leftFist;
    [SerializeField] private HandAnimationController rightFist;
    
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
        kenobi.StopDemo();
        displayActionMenu();
    }

    public override void TakeAction(Combatant target)
    {
        base.TakeAction(target);
        kenobi.DemoAbility(selectedAbility);
       
    }

    public override void TakeDefense(float duration)
    {
        base.TakeDefense(duration);
        kenobi.DemoAbility(selectedDefense);
    }

    private void MenuActionSelected(int arg0)
    {
        selectedAbility = actionAbilities[arg0];
        hideActionMenu();
        ActionSelected.Invoke(actionAbilities[arg0]);
        
    }
    
    public void displayActionMenu()
    {
        actionMenu.gameObject.SetActive(true);
        leftFist.Fist(true);
        rightFist.Fist(true);
        actionMenu.GetComponent<TargetPrefabHeightAdjust>().AdjustHeight();
    }

    public void hideActionMenu()
    {
        leftFist.Fist(false);
        rightFist.Fist(false);
        actionMenu.gameObject.SetActive(false);
    }
    
    protected override void ResetForNextTurn()
    {
        base.ResetForNextTurn();
        actionMenu.MenuActionSelected.RemoveListener(MenuActionSelected);
        kenobi.StopDemo();
    }
}
