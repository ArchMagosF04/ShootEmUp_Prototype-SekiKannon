using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private int damageToShields = 1;

    private Bullet_Controller controller;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(damageToShields);
        controller.DestroySelf();
        Debug.Log("Block");
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeSafeDamage(damageToShields);
        controller.DestroySelf();
        Debug.Log("Parry");
    }
}
