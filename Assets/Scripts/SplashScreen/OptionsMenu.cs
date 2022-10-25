using Button_UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplashScreen
{
    public class OptionsMenu : MonoBehaviour
    {
        public PlayerStatManager difficulty;

        [SerializeField] private GameObject easyRing;
        [SerializeField] private GameObject hardRing;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Splash Screen")
            {
                if (OptionButtons.ButtonChoice == 6)
                {
                    difficulty.difficulty = false;
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 7)
                {
                    difficulty.difficulty = true;
                    OptionButtons.ResetButtons();
                }else if (OptionButtons.ButtonChoice == 8)
                {
                    optionsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                    OptionButtons.ResetButtons();
                }
            }

            if (difficulty.difficulty)
            {
                hardRing.SetActive(true);
                easyRing.SetActive(false);
            }
            else
            {
                hardRing.SetActive(false);
                easyRing.SetActive(true);
            }
        }
    }
}
