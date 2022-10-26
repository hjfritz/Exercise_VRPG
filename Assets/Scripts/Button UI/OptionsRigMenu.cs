using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Button_UI
{
    public class OptionsRigMenu : MonoBehaviour
    {
        [SerializeField] private GameObject battleActionMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private InputActionReference rightSecondaryButton;
        [SerializeField] private TextMeshPro difficulty;

        public PlayerStatManager player;

        private bool menuOpen = false;
        private bool battleMenuWasOpen = false;
        [SerializeField] private HandAnimationController leftFist;
        [SerializeField] private HandAnimationController rightFist;

        private bool holdingFists = false;
        
        // Start is called before the first frame update
        void Start()
        {
            rightSecondaryButton.action.performed += OpenOptions;
            
            if (player.difficulty)
            {
                difficulty.text = "Easy";
            }
            else
            {
                difficulty.text = "Hard";
            }
        }

        private void OpenOptions(InputAction.CallbackContext obj)
        {
            if (!menuOpen)
            {
                menuOpen = true;
            }
            else
            {
                menuOpen = false;
            }
            
            Debug.Log(menuOpen);
        }

        // Update is called once per frame
        void Update()
        {
            //Opening and closing menu
            if (menuOpen)
            {
                optionsMenu.SetActive(true);
                if (battleActionMenu.activeInHierarchy)
                {
                    battleActionMenu.SetActive(false);
                    battleMenuWasOpen = true;
                }
                leftFist.Fist(true);
                rightFist.Fist(true);
                holdingFists = true;
                
                if (player.difficulty)
                {
                    difficulty.text = "Hard";
                }
                else
                {
                    difficulty.text = "Easy";
                }


            }
            else
            {
                optionsMenu.SetActive(false);
                if (holdingFists)
                {
                    leftFist.Fist(false);
                    rightFist.Fist(false);
                    holdingFists = false;
                }
                
            }

            if (battleMenuWasOpen && !menuOpen)
            {
                battleActionMenu.SetActive(true);
                battleMenuWasOpen = false;
                leftFist.Fist(true);
                rightFist.Fist(true);
            }
            
            //Button operations
            if (OptionButtons.ButtonChoice == 9)
            {
                Debug.Log("Saved");
                OptionButtons.ResetButtons();
            }else if (OptionButtons.ButtonChoice == 10)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                OptionButtons.ResetButtons();
            }else if (OptionButtons.ButtonChoice ==11)
            {
                if (player.difficulty)
                {
                    player.difficulty = false;
                    difficulty.text = "Easy";
                }
                else
                {
                    player.difficulty = true;
                    difficulty.text = "Hard";
                }
                OptionButtons.ResetButtons();
            }
        }
    }
}
