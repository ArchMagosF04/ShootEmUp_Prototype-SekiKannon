using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager Instance;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration = 1f;

    private Vector3 startPosition;

    private Coroutine shakeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        startPosition = transform.position;
    }

    public void ActivateShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        transform.position = startPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (Time.timeScale == 0f) elapsedTime = duration;

            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;

            yield return null;
        }

        transform.position = startPosition;
    }
}
