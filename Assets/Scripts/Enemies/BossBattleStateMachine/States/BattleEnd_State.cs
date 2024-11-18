using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnd_State : IState
{
    private StateMachine stateMachine;
    private BossBattleController controller;

    public BattleEnd_State(StateMachine stateMachine, BossBattleController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        Debug.Log("Boss End");
    }

    public void StateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
