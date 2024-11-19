using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MeteorShower : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] private float cooldownBetweenShots = 3f;

    private float shotsTimer;

    [SerializeField] private AbstractTurret[] asteroidColumn;

    private int previousAsteroids = 0;

    [SerializeField] private SoundLibraryObject soundLibrary;

    private void OnEnable()
    {
        shotsTimer = -2f;
    }

    private void Update()
    {
        if (isActive)
        {
            if (shotsTimer >= cooldownBetweenShots)
            {
                ShootAsteroid();
                shotsTimer = 0f;
            }
            else
            {
                shotsTimer += Time.deltaTime;
            }
        }
    }

    private void ShootAsteroid()
    {
        int index = 0;

        do
        {
            index = Random.Range(0, asteroidColumn.Length);
        }
        while (index == previousAsteroids);

        previousAsteroids = index;

        SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[0]).Play();
        asteroidColumn[index].Shoot();
    }
}
