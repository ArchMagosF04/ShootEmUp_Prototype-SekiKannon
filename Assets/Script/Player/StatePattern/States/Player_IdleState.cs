using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : IState
{
    private PlayerController playerController;

    public Player_IdleState(PlayerController player)
    {
        playerController = player;
    }

    public void OnEnter()
    {
        playerController.ChangeColor(Color.white);

        playerController.currentMoveSpeed = playerController.NormalMoveSpeed;
    }

    public void StateUpdate()
    {
        if (playerController.PlayerInput.IsShooting)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.attackState);
        }
        if (playerController.PlayerInput.IsShieldActive)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.parryState);
        }
    }

    public void OnExit()
    {
        // Debug.Log("Salio Idle");
    }
}
