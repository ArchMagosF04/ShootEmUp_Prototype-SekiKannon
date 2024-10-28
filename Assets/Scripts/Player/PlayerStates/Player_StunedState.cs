using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StunedState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float initialTimer = 1.5f; //Time in seconds which the player will spend in the stunned state.
    private float timer;

    public Player_StunedState(StateMachine machine, PlayerController playerController, Player_Shield shieldState)
    {
        this.stateMachine = machine;
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
        this.player_Shield = shieldState; //Get the reference to the player shield.
    }

    public void OnEnter()
    {
        timer = initialTimer; //Resets the timer.

        playerController.currentMoveSpeed = 0; //While in this state, the player's speed is reduced to 0 so they are unable to move.
    }

    public void StateUpdate()
    {
        if(timer <= 0) //Once the timer goes off, the player will return to the idle state.
        {
            stateMachine.ChangeState(playerController.IdleState);
        }

        timer -= Time.deltaTime;
    }

    public void OnExit() //When exiting the players shield is restored so it can be used again.
    {
        player_Shield.BreakShield(false);
        player_Shield.RestoreShield();
    }
}
