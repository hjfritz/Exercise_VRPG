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
    

    public UnityEvent<BattleAbility> ActionSelected = new UnityEvent<BattleAbility>();
    public UnityEvent DefenseSelected = new UnityEvent();
    public UnityEvent<int> TurnActionComplete = new UnityEvent<int>();
    public UnityEvent DeathConfirmed = new UnityEvent();

    public BattleAbility[] actionAbilities;
    public DefenseAbility[] defenseAbilities;
    protected BattleAbility selectedAbility;
    
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
        actionAbilities = GetComponents<BattleAbility>();
        //hard code to index 0 for now to avoid selecting the defense ability
        //int randomIndex = random.Next(0, actionAbilities.Length);
        selectedAbility = actionAbilities[0];
        ActionSelected.Invoke(actionAbilities[0]);
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
        }
    }
}
