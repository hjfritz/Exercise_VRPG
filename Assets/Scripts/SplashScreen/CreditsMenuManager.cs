using Button_UI;
using UnityEngine;

namespace SplashScreen
{
    public class CreditsMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject creditsMenu;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (OptionButtons.ButtonChoice == 12)
            {
                creditsMenu.SetActive(false);
                mainMenu.SetActive(true);
            }
        }
    }
}
