using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPhase_State : IState
{
    private StateMachine stateMachine;
    private BossBattleController controller;

    public ThirdPhase_State(StateMachine stateMachine, BossBattleController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        controller.CreateDreadnought();
    }

    public void StateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
