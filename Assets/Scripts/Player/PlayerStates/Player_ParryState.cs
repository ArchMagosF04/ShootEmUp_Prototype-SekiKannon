using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player_ParryState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float initialTimer = 0.2f; //Time in seconds in which the player will remain in the parry state before being taken to the shield state.
    private float speedMultiplier = 0.9f; //The speed is reduce to this % where 1.0f == 100%.
    private float timer;

    public Player_ParryState(StateMachine machine, PlayerController playerController, Player_Shield player_Shield) 
    {
        this.stateMachine = machine;
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
        this.player_Shield = player_Shield; //Get the reference to the player shield.
    }

    public void OnEnter()
    {
        timer = initialTimer;
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.
        player_Shield.ToggleShield(true); //Activate the shield.
        player_Shield.ToggleParry(true); //Sets the shield in it's parry mode.
    }

    public void StateUpdate()
    {
        if (!playerController.PlayerInput.IsShieldActive) //If the player releases the shield button, the shield will be deactivated and they will return to the idle state.
        {
            player_Shield.ToggleShield(false); //The shield is only turn off here, instead of onExit, since the other state transition requires for the shield to still be active.
            stateMachine.ChangeState(playerController.IdleState);
        }
        if(playerController.PlayerInput.IsShieldActive && timer < 0) //When the time runs out the player will be immediately sent to the shield state.
        {
            stateMachine.ChangeState(playerController.ShieldState);
        }

        timer -=Time.deltaTime; //Timer tick.
    }

    public void OnExit() //When exiting, it tells the shield that it is no longer in parry mode.
    {
        player_Shield.ToggleParry(false);
    }
}
