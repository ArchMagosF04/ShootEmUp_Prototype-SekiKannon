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

    private void Start()
    {
        frontBar.fillAmount = 1;
        backBar.fillAmount = 1;
    }

    private void OnEnable()
    {
        Player_Health.OnDamageReceived += UpdateBarValue;
    }

    public void UpdateBarValue(float maxValue, float currentValue)
    {
        float beforeChange = target;

        target = currentValue / maxValue;

        if (beforeChange > target)
        {
            StartCoroutine(GradualDamage());
        }else
        {
            StartCoroutine(GradualHeal());
        }
    }

    private IEnumerator GradualDamage()
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

    private IEnumerator GradualHeal()
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

    private void OnDisable()
    {
        Player_Health.OnDamageReceived -= UpdateBarValue;
    }
}
