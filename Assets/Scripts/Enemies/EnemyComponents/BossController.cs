using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public EnemyMovement Movement { get; private set; }
    public EnemyHealth health { get; private set; }

    //State Machine Components
    public StateMachine BossStateMachine { get; private set; }
    public Phase1_State Phase1State { get; private set; }
    public Phase2_State Phase2State { get; private set; }
    public Phase3_State Phase3State { get; private set; }
    public BossDeath_State BossDeathState { get; private set; }

    [SerializeField] private List<AbstractTurret> phase1Weapons = new List<AbstractTurret>();
    public List<AbstractTurret> Phase1Weapons => phase1Weapons;
    [SerializeField] private List<AbstractTurret> phase2Weapons = new List<AbstractTurret>();
    public List<AbstractTurret> Phase2Weapons => phase2Weapons;
    [SerializeField] private List<AbstractTurret> phase3Weapons = new List<AbstractTurret>();
    public List<AbstractTurret> Phase3Weapons => phase3Weapons;

    public Queue<AbstractTurret> Phase1Queue = new Queue<AbstractTurret>();


    private void Awake()
    {
        GameManager.Instance.SetBossReference(gameObject);

        BossStateMachine = new StateMachine();
        Phase1State = new Phase1_State(BossStateMachine, this);
        Phase2State = new Phase2_State(BossStateMachine, this);
        Phase3State = new Phase3_State(BossStateMachine, this);
        BossDeathState = new BossDeath_State(BossStateMachine, this);
    }

    private void Start()
    {
        BossStateMachine.Initialize(Phase1State);
    }

    private void Update()
    {
        BossStateMachine.Update();
    }

    public void ShuffleList(List<AbstractTurret> list)
    {
        List<AbstractTurret> temp = new List<AbstractTurret>();
        temp.AddRange(list);

        for (int i = 0; i < list.Count; i++)
        {
            int index = Random.Range(0, temp.Count);
            Phase1Queue.Enqueue(temp[index]);
            temp.RemoveAt(index);
        }
    }
}
