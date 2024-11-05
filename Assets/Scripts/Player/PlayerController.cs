using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player Stats")]

    [SerializeField] private float normalMoveSpeed;
    public float NormalMoveSpeed => normalMoveSpeed;
    [System.NonSerialized] public float currentMoveSpeed;



    //Components

    private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;

    private Rigidbody2D rb;



    //State Machine Components

    public StateMachine PlayerStateMachine { get; private set; }
    public Player_IdleState IdleState { get; private set; }
    public Player_AttackState AttackState { get; private set; }
    public Player_ParryState ParryState { get; private set; }
    public Player_ShieldState ShieldState { get; private set; }
    public Player_StunedState StunedState { get; private set; }



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



    [Header("Sound")]
    [SerializeField] private SoundLibraryObject soundLibrary;
    public SoundLibraryObject SoundLibrary => soundLibrary;


    private void Awake()
    {
        GameManager.Instance.SetPlayerReference(this.gameObject);
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        player_Shield = GetComponentInChildren<Player_Shield>();

        bulletPool = GetComponent<BulletPool>();
        bulletPool.SetBulletPrefab(bulletPrefab);

        soundLibrary.Initialize();

        PlayerStateMachine = new StateMachine();
        IdleState = new Player_IdleState(PlayerStateMachine, this);
        AttackState = new Player_AttackState(PlayerStateMachine, this);
        ParryState = new Player_ParryState(PlayerStateMachine, this, player_Shield);
        ShieldState = new Player_ShieldState(PlayerStateMachine, this, player_Shield);
        StunedState = new Player_StunedState(PlayerStateMachine, this, player_Shield);
    }

    private void Start()
    {
        currentMoveSpeed = normalMoveSpeed;
        PlayerStateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        PlayerStateMachine.Update(); //Updates the current state in the state machine.
        Movement();
    }

    private void Movement() //Handles the movement of the player.
    {
        rb.velocity = new Vector2 (playerInput.InputVector.x * currentMoveSpeed, playerInput.InputVector.y * currentMoveSpeed);
    }
}
