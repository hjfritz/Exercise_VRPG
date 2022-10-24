using System.Collections;
using System.Collections.Generic;
using ActionLayers.EnergyBallAttack;
using ActionLayers.ForceField;
using Training;
using UnityEngine;
using UnityEngine.Serialization;

public class Kenobi : MonoBehaviour
{
    [SerializeField] private TrainingObjects _trainingObjects;
    [SerializeField] private Animator _kenobi;
    
    void Squat()
    {
        StopAnimation();
        _kenobi.SetBool("SquatTraining", true);
    }

    void Twist()
    {
        StopAnimation();
        _kenobi.SetBool("TwistTraining", true);
        _trainingObjects.twisting = true;
    }

    void Magic()
    {
        StopAnimation();
        _kenobi.SetBool("EnergyBallTraining", true);
        _trainingObjects.energizing = true;
    }

    void Punch()
    {
        StopAnimation();
        _kenobi.SetBool("PunchTraining", true);
        _trainingObjects.punching = true;
    }

    void ForceField()
    {
        StopAnimation();
        _kenobi.SetBool("ForceFieldTraining", true);
        _trainingObjects.fielding = true;
    }

    public void DemoAbility(BattleAbility ability)
    {
        if (ability is SquatAbility)
        {
            Squat();
        }
        else if (ability is RussianTwistAbility)
        {
            
            Twist();
        }
        else if (ability is PunchAbility)
        {
            
            Punch();
        }
        else if (ability is EnergyBallAttack)
        {
            
            Magic();
        }
        else if (ability is ForceFieldDefense)
        {
            
            ForceField();
        }
    }
    public void StopDemo()
    {
        StopAnimation();
        
        _trainingObjects.punching = false;
        _trainingObjects.fielding = false;
        _trainingObjects.twisting = false;
        _trainingObjects.energizing = false;
    }

    private void StopAnimation()
    {
        _kenobi.SetBool("SquatTraining", false);
        _kenobi.SetBool("TwistTraining", false);
        _kenobi.SetBool("EnergyBallTraining", false);
        _kenobi.SetBool("PunchTraining", false);
        _kenobi.SetBool("ForceFieldTraining", false);
    }
}
