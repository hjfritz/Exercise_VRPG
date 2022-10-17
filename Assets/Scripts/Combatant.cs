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
    [SerializeField] private int maxHP = 100;
    [SerializeField] public ProgressBar healthBar;
    [SerializeField] public AbilityTimer abilityTimer;
    

    public UnityEvent<BattleAttackAbility> ActionSelected = new UnityEvent<BattleAttackAbility>();
    public UnityEvent<DefenseAbility> DefenseSelected = new UnityEvent<DefenseAbility>();
    public UnityEvent<int> TurnActionComplete = new UnityEvent<int>();
    public UnityEvent DeathConfirmed = new UnityEvent();

    public BattleAttackAbility[] actionAbilities;
    public DefenseAbility[] defenseAbilities;
    protected BattleAttackAbility selectedAbility;
    protected DefenseAbility selectedDefense;
    protected Combatant selectedTarget;
    
    private Random random = new Random();
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        defenseAbilities = GetComponents<DefenseAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SelectAction()
    {
        actionAbilities = GetComponents<BattleAttackAbility>();
       
        int randomIndex = random.Next(0, actionAbilities.Length);
        selectedAbility = actionAbilities[randomIndex];
        ActionSelected.Invoke(actionAbilities[randomIndex]);
    }

    public virtual void SelectDefense()
    {
        defenseAbilities = GetComponents<DefenseAbility>();
        selectedDefense = defenseAbilities[0];
        DefenseSelected.Invoke(selectedDefense);
    }

    public void TakeAction(Combatant target)
    {
        selectedTarget = target;
        selectedTarget.DeathConfirmed.AddListener(CancelActionOnDeath);
        selectedAbility.AbilityComplete.AddListener(CompleteTurnAction);
        abilityTimer.StartTimer(selectedAbility.abilityDuration);
        selectedAbility.ExecuteAction(target);
    }

    public void CancelActionOnDeath()
    {
        selectedAbility.FinalizeAction();
    }

    public void TakeDefense(float duration)
    {
        selectedDefense.ExecuteDefense(duration);
    }

    private void CompleteTurnAction(int attackPower)
    {
        ResetForNextTurn();
        TurnActionComplete.Invoke(attackPower);
        
    }

    protected virtual void ResetForNextTurn()
    {
        selectedAbility.AbilityComplete.RemoveListener(CompleteTurnAction);
        selectedTarget.DeathConfirmed.RemoveListener(CancelActionOnDeath);
    }

    public void UpdateHP(int newHP)
    {
        HP = newHP;
        healthBar.SetProgress(newHP, maxHP);
    }

    public int GetHP()
    {
        return HP;
    }

    public void HealToFullHealth()
    {
        UpdateHP(maxHP);
    }

    public void TakeDamage(int damage)
    {
        HP = Mathf.Max(0, HP - damage);
        healthBar.SetProgress(HP, maxHP);

        if (HP <= 0)
        {
            DeathConfirmed.Invoke();
            selectedDefense.FinalizeAction();
        }
    }

    public void TakeMitigatedDamage(int damage)
    {
        //Do some calculation here to factor in defense
        int mitigatedDamage = damage;
        TakeDamage(mitigatedDamage);
    }
}
