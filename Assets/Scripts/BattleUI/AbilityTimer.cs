using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityTimer : MonoBehaviour
{ 
    [SerializeField] private TMP_Text timerText;
    private float duration = 15;
    private float timer = 0;
    

    public void StartTimer(float duration)
    {
        this.duration = duration;
        timer = duration;
        timerText.gameObject.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timerText.text = $"{(int)Mathf.Ceil(timer)}";
            timer -= Time.deltaTime;
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
        
    }
}
