using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    [SerializeField] private Transform Spawnlocation;

    [Header ("1st Phase Elements")]
    [SerializeField] private FrigateController FrigatePrefab;
    public FrigateController Frigate {  get; private set; }

    [field: SerializeField] public P1MeteorShower P1Meteors { get; private set; }



    [Header("2nd Phase Elements")]

    [SerializeField] private BattlecruiserController BattlecruiserPrefab;
    public BattlecruiserController Battlecruiser { get; private set; }

    [field: SerializeField] public P2MeteorShower P2Meteors { get; private set; }


    //State Machine Components
    public StateMachine BattleStateMachine { get; private set; }
    public FirstPhase_State FirstPhase { get; private set; }
    public SecondPhase_State SecondPhase { get; private set; }
    public BattleEnd_State BattleEnd { get; private set; }

    public void Awake()
    {
        BattleStateMachine = new StateMachine();
        FirstPhase = new FirstPhase_State(BattleStateMachine, this);
        SecondPhase = new SecondPhase_State(BattleStateMachine, this);
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
        Frigate = Instantiate(FrigatePrefab, Spawnlocation.position, Spawnlocation.rotation);
        Frigate.OnEnemyDeath += EndPhase1;
    }

    public void CreateBattlecruiser()
    {
        Battlecruiser = Instantiate(BattlecruiserPrefab, Spawnlocation.position, Spawnlocation.rotation);
        Battlecruiser.OnEnemyDeath += EndPhase2;
    }

    private void BossEnd()
    {
        BattleStateMachine.ChangeState(BattleEnd);

        HUDManager.Instance.WinGame();
    }

    public void EndPhase1()
    {
        Frigate.OnEnemyDeath -= EndPhase1;
        Destroy(Frigate.gameObject);
        P1Meteors.isActive = false;
        BattleStateMachine.ChangeState(SecondPhase);
    }

    public void EndPhase2()
    {
        Battlecruiser.OnEnemyDeath -= EndPhase2;
        Destroy(Battlecruiser.gameObject);
        P2Meteors.isActive = false;
        BossEnd();
    }

    private void OnDisable()
    {
        if (Frigate != null)
        {
            Frigate.OnEnemyDeath -= EndPhase1;
        }
        if (Battlecruiser != null)
        {
            Battlecruiser.OnEnemyDeath -= EndPhase2;
        }
    }
}
