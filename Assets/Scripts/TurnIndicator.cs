using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text turnText;
    
    

    public void ShowTurnTaker(string actionTakerName)
    {
        turnText.text = $"{actionTakerName} Turn!";
       // StartCoroutine(AnimateAlpha());
        
    }
    
    //this doesn't work, need to lerp between colors, but i decided I didn't like it anyway
   /* private IEnumerator AnimateAlpha()
    {
        float fade = 0f;
        while (fade < 1f)
        {
            fade += Time.unscaledDeltaTime / 2f;
            turnText.alpha = Mathf.Lerp(100, 0, fade);
            yield return null;
        }

        turnText.alpha = 0;
    }*/
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
