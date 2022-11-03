using Button_UI;
using UnityEngine;

namespace SplashScreen
{
    public class LiabilityScreen : MonoBehaviour
    {
        [SerializeField] private GameObject liabilityScreen;
        [SerializeField] private GameObject mainMenu;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (OptionButtons.ButtonChoice == 20)
            {
                //Agree Button
                liabilityScreen.SetActive(false);
                mainMenu.SetActive(true);
                OptionButtons.ResetButtons();
            }else if (OptionButtons.ButtonChoice == 21)
            {
                //Quit Button
                
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                OptionButtons.ResetButtons();
            }
        }
    }
}
