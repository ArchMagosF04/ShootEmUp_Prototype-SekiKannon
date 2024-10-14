using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightShotTurret : MonoBehaviour, ITurret
{
    [SerializeField] private List<Transform> barrels = new List<Transform>();

    [SerializeField] private Bullet_Controller bulletPrefab;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    private void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());

        if(barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }
    }

    public void Shoot()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                Bullet_Controller creation = Instantiate(bulletPrefab);

                creation.transform.position = t.position;
                creation.transform.localRotation = t.rotation;

                creation.Bullet_Movement.Movement(t.up);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
