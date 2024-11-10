using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    public static HitStopManager Instance;

    private bool isWaiting = false;

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
    }

    public void FreezeFrame(float duration)
    {
        if (isWaiting) return;

        Time.timeScale = 0f;
        StartCoroutine(Wait(duration));
    }

    private IEnumerator Wait(float duration)
    {
        isWaiting = true;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        isWaiting = false;
    }
}
