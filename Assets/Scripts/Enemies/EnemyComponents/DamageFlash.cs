using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;
    [SerializeField] private AnimationCurve flashSpeedCurve;

    private SpriteRenderer sprite;
    private Material material;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        Initialize();
    }

    public void CallDamageFlash()
    {
        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private void Initialize()
    {
        material = sprite.material;
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColor();

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(0.5f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        material.SetColor("_FlashColor", flashColor);
    }

    private void SetFlashAmount(float amount)
    {
        material.SetFloat("_FlashAmount", amount);
    }
}
