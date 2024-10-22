using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject playerCharacter;
    public GameObject PlayerCharacter => playerCharacter;

    private GameObject bossCharacter;
    public GameObject BossCharacter => bossCharacter;

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

    public void SetPlayerReference(GameObject player)
    {
        playerCharacter = player;
    }

    public void SetBossReference(GameObject boss)
    {
        bossCharacter = boss;
    }
}
