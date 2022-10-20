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
    public GameObject[] abilityButtons;

    [SerializeField] private GameObject buttonPrefab;

    private XROrigin xrOrigin;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void ButtonClicked(int index)
    {
        MenuActionSelected.Invoke(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildMenu(BattleAbility[] abilities)
    {
        xrOrigin = FindObjectOfType<XROrigin>();
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        var buttonHeight = camera.transform.localPosition.y * .9f;
        Debug.Log(buttonHeight);
        if (abilityButtons != null)
        {
            foreach (var button in abilityButtons)
            {
                Destroy(button);
            }
        }
        
        abilityButtons = new GameObject[abilities.Length];
        float offset = (abilities.Length * .15f + ((abilities.Length - 1f) * .03f)) / 2f;
        
        for (int i = 0; i<abilities.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab,xrOrigin.gameObject.transform);
            //var facing = button.GetComponent<ButtonTextFacing>()._camera = camera;
            
            Vector3 position = new Vector3(-offset + (i * .21f), .4f, .5f);
            button.transform.localPosition = position + camera.transform.localPosition;
            button.transform.parent = this.transform;
            //RectTransform rect = button.GetComponent<RectTransform>();
            //rect.anchoredPosition = new Vector2(0, i * -170);

            BattleActionMenuButton battleActionMenuButton = button.GetComponentInChildren<BattleActionMenuButton>();
            battleActionMenuButton.id = i;
            battleActionMenuButton.BattleMenuButtonClicked.AddListener(ButtonClicked);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = $"{abilities[i].DisplayName}";
            
            abilityButtons[i] = button;
        }
    }

    
    
    
}
