using System.Collections;
using System.IO;
using Training;
using UnityEngine;

namespace Button_UI
{
    public class OptionButtons : MonoBehaviour
    {
        [SerializeField] private GameObject buttons;
        [SerializeField] private HandAnimationController leftFist;
        [SerializeField] private HandAnimationController rightFist;
        
        public static int ButtonChoice = 0;
        public static bool ButtonsOn = false;

        public bool holdingfists = false;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (ButtonsOn && !MapManager.map)
            {
                buttons.transform.LookAt(TrainingManager.currentTeacher.transform);
                buttons.SetActive(true);
                leftFist.Fist(true);
                rightFist.Fist(true);
                holdingfists = true;
            }else if (ButtonsOn && MapManager.map)
            {
                buttons.transform.LookAt(MapManager.look);
                buttons.SetActive(true);
                leftFist.Fist(true);
                rightFist.Fist(true);
                holdingfists = true;
            }
            else
            {
                if (holdingfists)
                {
                    leftFist.Fist(false);
                    rightFist.Fist(false);
                    holdingfists = false;
                }
                
                buttons.SetActive(false);
            }
        }

        public static void ResetButtons()
        {
            ButtonChoice = 0;
            ButtonsOn = false;
        }
    }
}
