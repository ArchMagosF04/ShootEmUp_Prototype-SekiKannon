using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShieldState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float speedMultiplier = 0.75f; //The speed is reduce to this % where 1.0f == 100%.

    public Player_ShieldState(StateMachine machine, PlayerController playerController, Player_Shield player_Shield)
    { 
        this.stateMachine = machine;
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
        this.player_Shield = player_Shield; //Get the reference to the player shield.
    }

    public void OnEnter()
    {
        player_Shield.ToggleShield(true);
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.
    }

    public void StateUpdate()
    {
        if (player_Shield.ShieldBroken) //If the shield runs out of durability it will break and send the player to the stunned state.
        {
            stateMachine.ChangeState(playerController.StunedState);
        }
        if (!playerController.PlayerInput.IsShieldActive) //If the player releases the shield button they will return to the idle state.
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
    }

    public void OnExit() //Deactivate the shield.
    {
        player_Shield.ToggleShield(false);
    }
}
