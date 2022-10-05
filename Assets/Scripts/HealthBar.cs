using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    private int health;
    

    public void UpdateHealth(int newHealth)
    {
        StartCoroutine(AnimateHealth(health, newHealth));
        health = newHealth;
    }

    private IEnumerator AnimateHealth(int oldhealth, int newhealth)
    {
        float fade = 0f;
        while (fade < 1f)
        {
            fade += Time.unscaledDeltaTime / .5f;
            healthText.text = $"HP: {(int)Mathf.Lerp(oldhealth, newhealth, fade)}";
            yield return null;
        }

        healthText.text = $"HP: {newhealth}";
    }
    
}
