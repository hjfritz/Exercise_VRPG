using Button_UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplashScreen
{
    public class GameMenuManager : MonoBehaviour
    {
        public PortalSceneChange portal;
        
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
                    portal.OpenPortal(0);
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 2)
                {
                    //Option Button
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 3)
                {
                    //Quit Button
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 4)
                {
                    //Load Button
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 5)
                {
                    //Credits Button
                    OptionButtons.ResetButtons();
                }
            }
        }
    }
}
