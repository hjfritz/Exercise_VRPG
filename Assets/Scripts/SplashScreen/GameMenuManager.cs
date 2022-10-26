using Button_UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplashScreen
{
    public class GameMenuManager : MonoBehaviour
    {
        public PortalSceneChange portal;
        
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject creditsMenu;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Splash Screen")
            {
                if (OptionButtons.ButtonChoice == 1)
                {
                    //Start Button
                    portal.OpenPortal(1);
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 2)
                {
                    //Option Button
                    optionsMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 3)
                {
                    //Quit Button
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 4)
                {
                    //Credits Button
                    creditsMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 5)
                {
                    //Load Button
                    OptionButtons.ResetButtons();
                }
            }
        }
    }
}
