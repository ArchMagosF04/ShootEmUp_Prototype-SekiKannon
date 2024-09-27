using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player_StateMachine
{
    private IState currentState;
    public IState CurrentState => currentState;

    //Define States
    public Player_IdleState idleState;
    public Player_AttackState attackState;
    public Player_ParryState parryState;
    public Player_ShieldState shieldState;
    public Player_StunedState stunedState;
    //---
    public Player_StateMachine(PlayerController playerController, Player_Shield player_Shield)
    {
        idleState = new Player_IdleState(playerController);
        attackState = new Player_AttackState(playerController);
        parryState = new Player_ParryState(playerController, player_Shield);
        shieldState = new Player_ShieldState(playerController, player_Shield);
        stunedState = new Player_StunedState(playerController, player_Shield);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        currentState.OnEnter();
    }

    public void ChangeState(IState nextState)
    {
        currentState.OnExit();
        currentState = nextState;
        currentState.OnEnter();

    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
    }
}
