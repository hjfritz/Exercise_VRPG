using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    public GameObject enemyprefab;
    public Transform enemySpawnPosition;
    public float repDuration = 1f;

    public EnemyANimationManager anim;

    private Combatant combatant;
    // Start is called before the first frame update
    void Start()
    {
        var enemy = Instantiate(enemyprefab, enemySpawnPosition.position, enemySpawnPosition.rotation);
        enemy.transform.parent = transform;
        anim = enemy.GetComponent<EnemyANimationManager>();
        combatant = GetComponent<Combatant>();
        
        combatant.ActionSelected.AddListener(LaunchAttack);
        combatant.DefenseSelected.AddListener(LaunchDefense);
        combatant.TurnActionComplete.AddListener(LaunchDefense2);
        combatant.DeathConfirmed.AddListener(anim.Death);

        combatant.GetComponent<DummyEnemyAtackAbility>().repDuration = repDuration;

    }


    private void LaunchAttack(BattleAttackAbility BA)
    {
        anim.Attack();
    }
    
    private void LaunchDefense(DefenseAbility DA)
    {
        anim.Defense();
    }

    private void LaunchDefense2()
    {
        anim.Defense();
    }

    public void StartAnim()
    {
        anim.StartAnim();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
