using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    [SerializeField] private Image leftBar;
    [SerializeField] private Image rightBar;

    [SerializeField] private Gradient barGradiant;

    [SerializeField] private float timeToDrain = 0.25f;
    private float target = 0;

    private Color newBarColor;

    private Coroutine lerpBar;

    private void Start()
    {
        leftBar.fillAmount = 0; 
        rightBar.fillAmount = 0;

        leftBar.color = barGradiant.Evaluate(target);
        rightBar.color = barGradiant.Evaluate(target);
    }

    private void OnEnable()
    {
        Player_Shield.OnDamageReceived += UpdateBarValue;
    }

    public void UpdateBarValue(float maxValue, float currentValue)
    {
        Debug.Log("Bar");

        target = currentValue/maxValue;

        lerpBar = StartCoroutine(GradualChange());

        CheckBarGradiantAmount();
    }

    private IEnumerator GradualChange()
    {
        float leftFillAmount = leftBar.fillAmount;
        float rightFillAmount = rightBar.fillAmount;

        Color currentLeftColor = leftBar.color;
        Color currentRightColor = rightBar.color;

        float elapsedTime = 0f;

        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            leftBar.fillAmount = Mathf.Lerp(leftFillAmount,target, (elapsedTime / timeToDrain));
            rightBar.fillAmount = Mathf.Lerp(rightFillAmount, target, (elapsedTime / timeToDrain));

            leftBar.color = Color.Lerp(currentLeftColor, newBarColor, (elapsedTime / timeToDrain));
            rightBar.color = Color.Lerp(currentRightColor, newBarColor, (elapsedTime / timeToDrain));

            yield return null;
        }
    }

    private void CheckBarGradiantAmount()
    {
        newBarColor = barGradiant.Evaluate(target);
    }

    private void OnDisable()
    {
        Player_Shield.OnDamageReceived -= UpdateBarValue;
    }
}
