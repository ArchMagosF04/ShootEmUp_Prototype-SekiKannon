using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1MeteorShower : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] private float cooldownBetweenWaves = 4f;
    [SerializeField] private float cooldownBetweenBarrages = 1f;

    private float wavesTimer;

    [SerializeField] private AbstractTurret[] topAsteroids;
    [SerializeField] private AbstractTurret[] bottomAsteroids;

    private List<AbstractTurret> firstBarrage = new List<AbstractTurret>();
    private List<AbstractTurret> secondBarrage = new List<AbstractTurret>();

    private List<int> temp = new List<int>();

    private void OnEnable()
    {
        wavesTimer = -5f;
    }

    private void Update()
    {
        if (isActive)
        {
            if (wavesTimer >= cooldownBetweenWaves)
            {
                StartCoroutine(MeteorShower());
                wavesTimer = 0f;
            }
            else
            {
                wavesTimer += Time.deltaTime;
            }
        }
    }

    private void ShuffleAsteroids()
    {
        bool rowSwitch = false;

        for (int i = 0; i < topAsteroids.Length; i++)
        {
            temp.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, temp.Count);

            if (rowSwitch)
            {
                firstBarrage.Add(topAsteroids[temp[index]]);
            }
            else
            {
                firstBarrage.Add(bottomAsteroids[temp[index]]);
            }

            if (i == 0)
            {
                secondBarrage.Add(bottomAsteroids[temp[index]]);
            }

            rowSwitch = !rowSwitch;
            temp.RemoveAt(index);
        }

        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, temp.Count);

            if (rowSwitch)
            {
                secondBarrage.Add(topAsteroids[temp[index]]);
            }
            else
            {
                secondBarrage.Add(bottomAsteroids[temp[index]]);
            }

            if (i == 0)
            {
                firstBarrage.Add(topAsteroids[temp[index]]);
            }

            rowSwitch = !rowSwitch;
            temp.RemoveAt(index);
        }

        temp.Clear();
    }

    private IEnumerator MeteorShower()
    {
        ShuffleAsteroids();

        for (int i = 0; i < firstBarrage.Count; i++)
        {
            firstBarrage[i].Shoot();
        }

        yield return new WaitForSeconds(cooldownBetweenBarrages);

        for (int i = 0; i < secondBarrage.Count; i++)
        {
            secondBarrage[i].Shoot();
        }

        firstBarrage.Clear();
        secondBarrage.Clear();
    }
}
