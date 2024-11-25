using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertIconSpawn : MonoBehaviour, IPreFireEffect
{
    [SerializeField] private Transform iconPosition;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float iconLifeTime = 0.5f;

    public void ExecuteEffect()
    {
        GameObject icon = Instantiate(iconPrefab, iconPosition.position, Quaternion.identity);

        Destroy(icon, iconLifeTime);
    }
}
