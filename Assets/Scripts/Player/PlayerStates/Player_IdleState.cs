using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController;

    public Player_IdleState(StateMachine machine, PlayerController player)
    {
        this.stateMachine = machine;
        playerController = player; //Get the reference to the player controller who holds the state machine.
    }

    public void OnEnter()
    {
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed; //Set the movement speed to its normal value.
    }

    public void StateUpdate()
    {
        if (playerController.PlayerInput.IsShooting) //if the player presses the shot button, then they will transition to the attack state.
        {
            stateMachine.ChangeState(playerController.AttackState);
        }
        if (playerController.PlayerInput.IsShieldActive) //if the player presses the shield button, then they will transition to the parry state.
        {
            stateMachine.ChangeState(playerController.ParryState);
        }
    }

    public void OnExit()
    {

    }
}