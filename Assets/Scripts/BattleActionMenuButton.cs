using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleActionMenuButton : MonoBehaviour
{
    public int id;

    public UnityEvent<int> BattleMenuButtonClicked;

    private Button button;
    
    // Start is called before the first frame update
   /* void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickListener);
    }

    private void ClickListener()
    {
        BattleMenuButtonClicked.Invoke(id);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        //tempbugfix - temporary fix for the non autonomous combat manager.  limits to the colliders on the hands
        //Character controller collider was triggering the energy ball ability by accident due to the repositioning
        if (other.CompareTag("Player"))
        {
            BattleMenuButtonClicked.Invoke(id);
        }
    }
}
