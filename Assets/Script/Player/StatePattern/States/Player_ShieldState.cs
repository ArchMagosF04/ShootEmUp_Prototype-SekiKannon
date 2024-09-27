using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShieldState : IState
{
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float speedMultiplier = 0.75f;

    public Player_ShieldState(PlayerController playerController, Player_Shield player_Shield)
    {
        this.playerController = playerController;
        this.player_Shield = player_Shield;
    }

    public void OnEnter()
    {
        playerController.ChangeColor(Color.blue);

        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier;
    }

    public void StateUpdate()
    {
        if (!playerController.PlayerInput.IsShieldActive)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }
        if(player_Shield.shieldBroken)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.stunedState);
        }
    }

    public void OnExit()
    {
        playerController.ShieldComponent.enabled = false;
    }
}
