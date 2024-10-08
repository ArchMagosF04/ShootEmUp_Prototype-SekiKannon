using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackState : IState
{
    private PlayerController playerController; 

    [SerializeField] private float shootingInterval = 0.1f; //Time in seconds between each shot.
    private float shootCooldown;
    private float speedMultiplier = 0.9f; //The speed is reduce to this % where 1.0f == 100%.

    public Player_AttackState(PlayerController playerController)
    {
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
    }

    public void OnEnter()
    {
        shootCooldown = 0f;
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.%
    }

    public void StateUpdate()
    {
        ShootBullet(); //While in this state, the player will continuously shoot bullets; 

        if (!playerController.PlayerInput.IsShooting) //if the player stops pressing the shot button, then they will return to the idle state.
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }
        if (playerController.PlayerInput.IsShieldActive) //if the player presses the shield button, then they will transition to the parry state.
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.parryState);
        }
    }

    public void OnExit()
    {

    }

    private void ShootBullet()
    {
        if (shootCooldown <= 0) //When the cooldown is done, the player will spawn a pair of bullets.
        {
            playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[0]);
            playerController.BulletPool.pool.Get();
            playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[1]);
            playerController.BulletPool.pool.Get();

            shootCooldown = shootingInterval; //Activate the cooldown.
        }
        else
        {
            shootCooldown -= Time.deltaTime; //Cooldown tick.
        }
    }
}
