using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image frontBar;
    [SerializeField] private Image backBar;

    [SerializeField] private float timeToDrain = 1f;
    private float target;

    private Coroutine damageRoutine;
    private Coroutine healRoutine;

    private void Start()
    {
        frontBar.fillAmount = 1;
        backBar.fillAmount = 1;
    }

    private void OnEnable()
    {
        Player_Health.OnDamageReceived += DecreaseBar;
        Player_Health.OnHealthHealed += IncreaseBar;
    }

    public void DecreaseBar(float maxValue, float currentValue)
    {
        float beforeChange = target;

        target = currentValue / maxValue;

        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
        }
        
        damageRoutine = StartCoroutine(GradualDamage());
    }

    private void IncreaseBar(float maxValue, float currentValue)
    {
        float beforeChange = target;

        target = currentValue / maxValue;

        if (healRoutine != null)
        {
            StopCoroutine(healRoutine);
        }

        healRoutine = StartCoroutine(GradualHeal());
    }

    private IEnumerator GradualDamage() //The main bar instantly goes to its new value, and the bar on the back lingers for a bit to show the amount of damage done.
    {
        backBar.color = Color.white;
        float backFill = backBar.fillAmount;

        float elapsedTime = 0f;

        frontBar.fillAmount = target;

        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            backBar.fillAmount = Mathf.Lerp(backFill, target, (elapsedTime / timeToDrain));

            yield return null;
        }
    }

    private IEnumerator GradualHeal() //The bar on the back turns green and instantly goes to its new value, the main bar lingers for a bit to show the amount heal.
    {
        backBar.color = Color.green;
        float frontFill = frontBar.fillAmount;

        float elapsedTime = 0f;

        backBar.fillAmount = target;

        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            frontBar.fillAmount = Mathf.Lerp(frontFill, target, (elapsedTime / timeToDrain));

            yield return null;
        }
    }

    private void OnDisable() //Unsubscribes from the event.
    {
        Player_Health.OnDamageReceived -= DecreaseBar;
        Player_Health.OnHealthHealed -= IncreaseBar;
    }
}
