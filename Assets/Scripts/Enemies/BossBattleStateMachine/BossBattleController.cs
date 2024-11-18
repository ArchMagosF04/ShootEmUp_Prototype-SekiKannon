using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    [Header ("1st Phase Elements")]
    [SerializeField] private FrigateController FrigatePrefab;
    public FrigateController Frigate {  get; private set; }

    private EnemyHealth frigateHealth;

    [SerializeField] private Transform FrigateLocation;

    [field: SerializeField] public P1MeteorShower P1Meteors { get; private set; }



    //State Machine Components
    public StateMachine BattleStateMachine { get; private set; }
    public FirstPhase_State FirstPhase { get; private set; }
    public BattleEnd_State BattleEnd { get; private set; }

    public void Awake()
    {
        BattleStateMachine = new StateMachine();
        FirstPhase = new FirstPhase_State(BattleStateMachine, this);
        BattleEnd = new BattleEnd_State(BattleStateMachine, this);
    }

    private void Start()
    {
        BattleStateMachine.Initialize(FirstPhase);
    }

    private void Update()
    {
        BattleStateMachine.Update();
    }

    public void CreateFrigate()
    {
        Frigate = Instantiate(FrigatePrefab, FrigateLocation.position, FrigateLocation.rotation);
        frigateHealth = Frigate.GetComponent<EnemyHealth>();
        frigateHealth.OnEnemyDeath += BossEnd;
    }

    private void BossEnd()
    {
        frigateHealth.OnEnemyDeath -= BossEnd;
        BattleStateMachine.ChangeState(BattleEnd);
    }

    public void EndPhase1()
    {
        Destroy(Frigate.gameObject);
        P1Meteors.isActive = false;
    }

    private void OnDisable()
    {
        if(frigateHealth != null)
        {
            frigateHealth.OnEnemyDeath -= BossEnd;
        }
    }
}
