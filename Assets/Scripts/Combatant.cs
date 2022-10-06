using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class Combatant : MonoBehaviour
{
    [SerializeField] public string displayName = "Enemy";
    [SerializeField] private int HP;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] public AbilityTimer abilityTimer;
    

    public UnityEvent<BattleAbility> ActionSelected = new UnityEvent<BattleAbility>();
    public UnityEvent DefenseSelected = new UnityEvent();
    public UnityEvent<int> TurnActionComplete = new UnityEvent<int>();

    public BattleAbility[] actionAbilities;
    protected BattleAbility selectedAbility;
    
    private Random random = new Random();
    
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

        int randomIndex = random.Next(0, actionAbilities.Length);
        selectedAbility = actionAbilities[randomIndex];
        ActionSelected.Invoke(actionAbilities[randomIndex]);
    }

    public void TakeAction()
    {
        selectedAbility.AbilityComplete.AddListener(CompleteTurnAction);
        abilityTimer.StartTimer(selectedAbility.abilityDuration);
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

    public void UpdateHP(int newHP)
    {
        HP = newHP;
        healthBar.UpdateHealth(newHP);
    }

    public int GetHP()
    {
        return HP;
    }
}
