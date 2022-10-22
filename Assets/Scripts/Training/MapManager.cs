using Button_UI;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Training
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private XRSocketInteractor island;
        [SerializeField] private XRSocketInteractor mountain;

        public PortalSceneChange sceneChange;

        private bool level1Option = false;
        private bool islandOption = false;
        
        public static bool map = false;
        public static Transform look;
        
        // Start is called before the first frame update
        void Start()
        {
            mountain.selectEntered.AddListener(GotoLevel1);
            island.selectEntered.AddListener(GoToIsland);
            look = mountain.transform;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (islandOption && OptionButtons.ButtonChoice == 1)
            {
                
            }else if (level1Option && OptionButtons.ButtonChoice == 1)
            {
                level1Option = false;
                OptionButtons.ResetButtons();
                sceneChange.OpenPortal(1);
            }
        }

        private void GotoLevel1(SelectEnterEventArgs arg0)
        {
            level1Option = true;
            OptionButtons.ButtonsOn = true;
        }

        private void GoToIsland(SelectEnterEventArgs arg0)
        {
            //throw new System.NotImplementedException();
        }

        
    }
}
