using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerMenu : MonoBehaviour
{
    [SerializeField] private Canvas welcomeCanvas;
    [SerializeField] private Canvas restartCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWelcomeCanvas()
    {
        restartCanvas.gameObject.SetActive(false);
        welcomeCanvas.gameObject.SetActive(true);
    }

    public void ShowRestartCanvas()
    {
        restartCanvas.gameObject.SetActive(true);
        welcomeCanvas.gameObject.SetActive(false);
    }

    public void HideMenu()
    {
        restartCanvas.gameObject.SetActive(false);
        welcomeCanvas.gameObject.SetActive(false);
    }
}
