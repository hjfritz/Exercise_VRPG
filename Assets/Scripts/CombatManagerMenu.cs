using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManagerMenu : MonoBehaviour
{
    [SerializeField] private Canvas welcomeCanvas;
    [SerializeField] private Canvas restartCanvas;
    [SerializeField] private Canvas battleHudCanvas;

    [SerializeField] public TurnIndicator turnIndicator;
    [SerializeField] public TMP_Text winnerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void ShowWelcomeCanvas()
    {
        restartCanvas.gameObject.SetActive(false);
        welcomeCanvas.gameObject.SetActive(true);
        battleHudCanvas.gameObject.SetActive(false);
    }

    public void ShowRestartCanvas(string winnerName)
    {
        winnerText.text = $"{winnerName} wins";
        restartCanvas.gameObject.SetActive(true);
        welcomeCanvas.gameObject.SetActive(false);
        battleHudCanvas.gameObject.SetActive(false);
    }
    */
   
    public void ShowBattleHudCanvas()
    {
        restartCanvas.gameObject.SetActive(false);
        welcomeCanvas.gameObject.SetActive(false);
        battleHudCanvas.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        restartCanvas.gameObject.SetActive(false);
        welcomeCanvas.gameObject.SetActive(false);
        battleHudCanvas.gameObject.SetActive(false);
    }
}
