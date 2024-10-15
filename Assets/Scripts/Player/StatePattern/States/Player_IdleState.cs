using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : IState
{
    private PlayerController playerController;

    public Player_IdleState(PlayerController player)
    {
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
            playerController.StateMachine.ChangeState(playerController.StateMachine.attackState);
        }
        if (playerController.PlayerInput.IsShieldActive) //if the player presses the shield button, then they will transition to the parry state.
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.parryState);
        }
    }

    public void OnExit()
    {

    }
}
