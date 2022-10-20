using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Training
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private XRSocketInteractor island;
        [SerializeField] private XRSocketInteractor mountain;
        
        // Start is called before the first frame update
        void Start()
        {
            mountain.selectEntered.AddListener(GotoLevel1);
            island.selectEntered.AddListener(GoToIsland);
        }

        private void GotoLevel1(SelectEnterEventArgs arg0)
        {
            GameManager.OpenPortal();
        }

        private void GoToIsland(SelectEnterEventArgs arg0)
        {
            throw new System.NotImplementedException();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
