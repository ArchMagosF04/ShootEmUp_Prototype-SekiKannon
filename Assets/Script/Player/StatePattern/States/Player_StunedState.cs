using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StunedState : IState
{
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float initialTimer = 1.5f;
    private float timer;

    public Player_StunedState(PlayerController playerController, Player_Shield shieldState)
    {
        this.playerController = playerController;
        this.player_Shield = shieldState;
    }

    public void OnEnter()
    {
        playerController.ChangeColor(Color.yellow);
        timer = initialTimer;

        playerController.currentMoveSpeed = 0;
    }

    public void StateUpdate()
    {
        if(timer <= 0)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }

        timer -= Time.deltaTime;
    }

    public void OnExit()
    {
        player_Shield.shieldBroken = false;
        player_Shield.RestoreShield();
    }
}
