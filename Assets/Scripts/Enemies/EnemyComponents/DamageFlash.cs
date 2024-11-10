using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color[] flashColor;
    [SerializeField, Range(0f, 1f)] private float flashTime = 0.25f;
    [SerializeField] private AnimationCurve flashSpeedCurve;

    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        Initialize();
    }

    public void CallDamageFlash(int index)
    {
        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }
        damageFlashCoroutine = StartCoroutine(DamageFlasher(index));
    }

    private void Initialize()
    {
        materials = new Material[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    private IEnumerator DamageFlasher(int index)
    {
        SetFlashColor(index);

        float currentFlashAmount = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = flashSpeedCurve.Evaluate(elapsedTime / flashTime);
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor(int colorIndex)
    {
        for (int i = 0;i < materials.Length;i++)
        {
            materials[i].SetColor("_FlashColor", flashColor[colorIndex]);
        }
    }

    private void SetFlashAmount(float amount)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
