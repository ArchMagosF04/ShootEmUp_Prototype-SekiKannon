using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Player_AttackState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController; 

    [SerializeField] private float shootingInterval = 0.07f; //Time in seconds between each shot.
    private float shootCooldown;
    private float speedMultiplier = 0.9f; //The speed is reduce to this % where 1.0f == 100%.

    public Player_AttackState(StateMachine machine, PlayerController playerController)
    {
        this.stateMachine = machine;
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
    }

    public void OnEnter()
    {
        playerController.WeaponAnimator.SetBool("IsAttacking", true);
        shootCooldown = 0f;
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.%
    }

    public void StateUpdate()
    {
        ShootBullet(); //While in this state, the player will continuously shoot bullets; 

        if (!playerController.PlayerInput.IsShooting) //if the player stops pressing the shot button, then they will return to the idle state.
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
        if (playerController.PlayerInput.IsShieldActive) //if the player presses the shield button, then they will transition to the parry state.
        {
            stateMachine.ChangeState(playerController.ParryState);
        }
    }

    public void OnExit()
    {
        playerController.WeaponAnimator.SetBool("IsAttacking", false);
    }

    private void ShootBullet()
    {
        if (shootCooldown <= 0) //When the cooldown is done, the player will spawn a pair of bullets.
        {
            foreach (Transform barrel in playerController.Cannons)
            {
                playerController.BulletPool.ChangeSpawnpoint(barrel);
                Bullet_Controller bullet = playerController.BulletPool.pool.Get();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.Bullet_Movement.Movement(barrel.up);
            }
            //playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[0]);
            //Bullet_Controller bullet1 = playerController.BulletPool.pool.Get();
            //playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[1]);
            //Bullet_Controller bullet2 = playerController.BulletPool.pool.Get();

            shootCooldown = shootingInterval; //Activate the cooldown.
        }
        else
        {
            shootCooldown -= Time.deltaTime; //Cooldown tick.
        }
    }
}
