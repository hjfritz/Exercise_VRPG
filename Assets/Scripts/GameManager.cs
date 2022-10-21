using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void OpenPortal(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
