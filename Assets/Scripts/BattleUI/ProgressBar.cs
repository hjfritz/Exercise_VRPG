using System.Collections;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image ProgressImage;
    [SerializeField]
    private float DefaultSpeed = 1f;
    [SerializeField] private TMP_Text label;

    private Coroutine AnimationCoroutine;

    private void Start()
    {
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}'s ProgressImage is not of type \"Filled\" so it cannot be used as a progress bar. Disabling this Progress Bar.");
            enabled = false;
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this.gameObject);
#endif
        }
    }

    public void SetProgress(int HP, int maxHP)
    {
        SetProgress(HP, maxHP,  DefaultSpeed);
    }

    public void SetProgress(int HP, int maxHP, float Speed)
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
    }

    private IEnumerator AnimateProgress(int HP, int maxHP, float Speed)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;
        float Progress = (float)HP / maxHP;
        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * Speed;

            label.text = $"HP {HP}/{maxHP}";
            yield return null;
        }

        ProgressImage.fillAmount = Progress;
        label.text = $"HP {HP}/{maxHP}";
    }
}
