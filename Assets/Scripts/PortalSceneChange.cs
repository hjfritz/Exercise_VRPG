using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneChange : MonoBehaviour
{
    public PortalFadeScreen fadeScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenPortal(int scene)
    {
        StartCoroutine(GoToSceneRoutine(scene));
    }

    public void GoToScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(sceneIndex);
    }
}
