using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleActionMenu : MonoBehaviour
{
   // public Combatant actionTaker;
    public UnityEvent<int> MenuActionSelected = new UnityEvent<int>();
    public Button[] abilityButtons;

    [SerializeField] private Button buttonPrefab;

    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void ButtonClicked()
    {
        Debug.Log("ButtonClicked");
        MenuActionSelected.Invoke(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildMenu(BattleAbility[] abilities)
    {
        canvas = GetComponentInChildren<Canvas>();
        
        if (abilityButtons != null)
        {
            foreach (var button in abilityButtons)
            {
                Destroy(button);
            }
        }
        
        abilityButtons = new Button[abilities.Length];
        
        for (int i = 0; i<abilities.Length; i++)
        {
            Button button = Instantiate(buttonPrefab,canvas.GetComponent<RectTransform>());

            RectTransform rect = button.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, i * 40);
            
            button.onClick.AddListener(ButtonClicked);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = $"{abilities[i].DisplayName}";
            
            abilityButtons[i] = button;
        }
    }

    
    
    
}
