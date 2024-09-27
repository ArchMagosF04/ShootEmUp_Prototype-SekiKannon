using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackState : IState
{
    private PlayerController playerController;

    [SerializeField] private float shootingInterval = 0.1f;
    private float shootCooldown;

    public Player_AttackState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OnEnter()
    {
        shootCooldown = 0f;
        playerController.ChangeColor(Color.red);
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * 0.90f;
    }

    public void StateUpdate()
    {
        ShootBullet();

        if (!playerController.PlayerInput.IsShooting)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.idleState);
        }
        if (playerController.PlayerInput.IsShieldActive)
        {
            playerController.StateMachine.ChangeState(playerController.StateMachine.parryState);
        }
    }

    public void OnExit()
    {

    }

    private void ShootBullet()
    {
        if (shootCooldown <= 0)
        {
            playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[0]);
            playerController.BulletPool.pool.Get();
            playerController.BulletPool.ChangeSpawnpoint(playerController.Cannons[1]);
            playerController.BulletPool.pool.Get();

            shootCooldown = shootingInterval;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }
}
