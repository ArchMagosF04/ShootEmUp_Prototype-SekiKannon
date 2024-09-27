using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player_ParryState : IState
{
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float initialTimer = 0.2f;
    private float speedMultiplier = 0.9f;
    private float timer;

    public Player_ParryState(PlayerController playerController, Player_Shield player_Shield)
    {
        this.playerController = playerController;
        this.player_Shield = player_Shield;
    }

    public void OnEnter()
    {
        playerController.ChangeColor(Color.green);
        timer = initialTimer;
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier;
        playerController.ShieldComponent.enabled = true;
        player_Shield.isParryActive = true;
    }

    public void StateUpdate()
    {
        if (!playerController.PlayerInput.IsShieldActive)
        {
            playerController.ShieldComponent.enabled = false;
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }
        if(playerController.PlayerInput.IsShieldActive && timer < 0)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.shieldState);
        }

        timer -=Time.deltaTime;
    }

    public void OnExit()
    {
        player_Shield.isParryActive = false;
    }
}
