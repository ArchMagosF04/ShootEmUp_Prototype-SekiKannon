using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShieldState : IState
{
    private PlayerController playerController;
    private Player_Shield player_Shield;

    private float speedMultiplier = 0.75f; //The speed is reduce to this % where 1.0f == 100%.

    public Player_ShieldState(PlayerController playerController, Player_Shield player_Shield)
    { 
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
        this.player_Shield = player_Shield; //Get the reference to the player shield.
    }

    public void OnEnter()
    {
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.
    }

    public void StateUpdate()
    {
        if (!playerController.PlayerInput.IsShieldActive) //If the player releases the shield button they will return to the idle state.
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }
        if(player_Shield.shieldBroken) //If the shield runs out of durability it will break and send the player to the stunned state.
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.stunedState);
        }
    }

    public void OnExit() //Deactivate the shield.
    {
        player_Shield.ToggleShield(false);
    }
}
