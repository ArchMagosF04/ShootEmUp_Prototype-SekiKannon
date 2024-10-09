using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player Stats")]

    [SerializeField] private float normalMoveSpeed;
    public float NormalMoveSpeed => normalMoveSpeed;
    [System.NonSerialized] public float currentMoveSpeed;



    [Header ("Components")]

    private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;

    private Rigidbody2D rb;



    [Header("State Machine")]

    private Player_StateMachine stateMachine;
    public Player_StateMachine StateMachine => stateMachine;



    [Header("Attack State")]

    [SerializeField] private Bullet_Controller bulletPrefab;

    [SerializeField] private Animator weaponAnimator;
    public Animator WeaponAnimator => weaponAnimator;

    [SerializeField] private Transform[] cannons;
    public Transform[] Cannons => cannons;

    private BulletPool bulletPool;
    public BulletPool BulletPool => bulletPool;

   

    [Header ("Shield Components")]

    private Player_Shield player_Shield;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        player_Shield = GetComponentInChildren<Player_Shield>();
        stateMachine = new Player_StateMachine(this, player_Shield);
        bulletPool = GetComponent<BulletPool>();
        bulletPool.SetBulletPrefab(bulletPrefab);
    }

    private void Start()
    {
        currentMoveSpeed = normalMoveSpeed;
        stateMachine.Initialize(stateMachine.idleState);
    }

    private void Update()
    {
        stateMachine.Update(); //Updates the current state in the state machine.
        Movement();
    }

    private void Movement() //Handles the movement of the player.
    {
        rb.velocity = new Vector2 (playerInput.InputVector.x * currentMoveSpeed, playerInput.InputVector.y * currentMoveSpeed);
    }
}
