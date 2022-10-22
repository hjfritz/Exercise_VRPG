using System.Collections;
using System.Collections.Generic;
using Button_UI;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleActionMenu : MonoBehaviour
{
   // public Combatant actionTaker;
    public UnityEvent<int> MenuActionSelected = new UnityEvent<int>();

    // Start is called before the first frame update
    void Start()
    {
        BattleActionMenuButton[] buttons = GetComponentsInChildren<BattleActionMenuButton>();
        foreach (var button in buttons)
        {
            button.BattleMenuButtonClicked.AddListener(ButtonClicked);
            Debug.Log($"{button.name} listener set");
        }
        gameObject.SetActive(false);
    }

    public void ButtonClicked(int index)
    {
        Debug.Log($"index is {index}");
        MenuActionSelected.Invoke(index);
    }

   

    
    
    
}
