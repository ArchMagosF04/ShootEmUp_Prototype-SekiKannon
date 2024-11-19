using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhase_State : IState
{
    private StateMachine stateMachine;
    private BossBattleController controller;

    public SecondPhase_State(StateMachine stateMachine, BossBattleController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        controller.CreateBattlecruiser();
        controller.P2Meteors.isActive = true;
    }

    public void StateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
