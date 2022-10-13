using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;
using UnityEngine;

public class BattleAction
{
    public Combatant actionTaker;
    public Combatant actionTarget;
    public BattleAttackAbility selectedAbility;
    public DefenseAbility selectedDefense;
    public int attackPower;
    public int defensePower;
    public bool attackComplete;
    public bool defenseComplete;
}
