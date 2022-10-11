using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimer : MonoBehaviour
{
    [SerializeField]
    private Image ProgressImage;
    [SerializeField]
    private float DefaultSpeed = .02f;
    [SerializeField] private TMP_Text timerText;
    private float duration = 15;
    private float timer = 0;
    private Coroutine AnimationCoroutine;

    private void Start()
    {
        gameObject.SetActive(false);
        
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}'s ProgressImage is not of type \"Filled\" so it cannot be used as a progress bar. Disabling this Progress Bar.");
            enabled = false;
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this.gameObject);
#endif
        }
    }

    public void StartTimer(float duration)
    {
        this.duration = duration;
        timer = duration;
        gameObject.SetActive(true);
    }

   

   /* public void SetProgress(int HP, int maxHP, float Speed)
    {
        var Progress = (float)HP / maxHP;
        if (Progress < 0 || Progress > 1)
        {
            Debug.LogWarning($"Invalid progress passed, expected value is between 0 and 1, got {Progress}. Clamping.");
            Progress = Mathf.Clamp01(Progress);
        }
        if (Progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateProgress(HP, maxHP, Speed));
        }
    }*/
   
   // Update is called once per frame
   void Update()
   {
       if (timer > 0)
       {
           timer -= Time.deltaTime;
           ProgressImage.fillAmount = 1f-timer/duration;
           timerText.text = $"{Mathf.CeilToInt(timer)}";
           //StartCoroutine(AnimateProgress(timer/duration));
       }
       else
       {
           gameObject.SetActive(false);
       }
        
   }

    private IEnumerator AnimateProgress(float Progress)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;
        
        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * DefaultSpeed;

            timerText.text = $"{Mathf.CeilToInt(timer)}";
            yield return null;
        }

        ProgressImage.fillAmount = Progress;
        timerText.text = $"{Mathf.CeilToInt(timer)}";
    }
}
