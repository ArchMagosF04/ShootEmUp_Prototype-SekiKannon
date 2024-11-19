using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase_State : IState
{
    private StateMachine stateMachine;
    private BossBattleController controller;

    public FirstPhase_State(StateMachine stateMachine, BossBattleController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        controller.CreateFrigate();
        controller.P1Meteors.isActive = true;
    }

    public void StateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
