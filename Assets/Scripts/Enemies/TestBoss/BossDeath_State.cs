using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath_State : IState
{
    private BossController controller;
    private StateMachine stateMachine;

    public BossDeath_State(StateMachine machine, BossController controller)
    {
        stateMachine = machine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        controller.Movement.SetSpeedMultiplier(0);
        controller.EngineSprite.SetActive(false);
        controller.ShipAnimator.SetBool("IsDead", true);
        controller.DestroyTurrets();
    }

    public void StateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
