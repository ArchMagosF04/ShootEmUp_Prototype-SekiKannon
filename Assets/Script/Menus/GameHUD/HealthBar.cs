using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private bool isChangeGradual = false;
    [SerializeField] private float timeToDrain = 0.25f;
    private float target;

    private Coroutine drainHealthBarCoroutine;

    public void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth, float health)
    {
        slider.maxValue = maxHealth;
        SetHealth(health);
    }

    public void SetHealth(float health)
    {
        if(!isChangeGradual)
        {
            slider.value = health;
        }else
        {
            target = health;
            drainHealthBarCoroutine = StartCoroutine(DrainHealth());
        }
    }

    private IEnumerator DrainHealth()
    {
        float initialFill = slider.value;
        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime+= Time.deltaTime;

            slider.value = Mathf.Lerp(initialFill, target, (elapsedTime / timeToDrain));

            yield return null;
        }
    }
}
