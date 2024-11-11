using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_AttackState : IState
{
    private StateMachine stateMachine;
    private PlayerController playerController; 

    private float shootingInterval = 0.07f; //Time in seconds between each shot.
    private float shootCooldown;
    private float speedMultiplier = 0.9f; //The speed is reduce to this % where 1.0f == 100%.
    private float parryCooldown = 0.15f;
    private float canParryTimer;

    public Player_AttackState(StateMachine machine, PlayerController playerController)
    {
        this.stateMachine = machine;
        this.playerController = playerController; //Get the reference to the player controller who holds the state machine.
    }

    public void OnEnter()
    {
        canParryTimer = parryCooldown;
        playerController.WeaponAnimator.SetBool("IsAttacking", true);
        shootCooldown = 0f;
        playerController.currentMoveSpeed = playerController.NormalMoveSpeed * speedMultiplier; //Reduce the player's speed.%
    }

    public void StateUpdate()
    {
        BulletSound();
        playerController.CurrentWeapon.Shoot();

        ParryCountdown();

        if (playerController.PlayerInput.IsShieldActive && canParryTimer <= 0f) //if the player presses the shield button, then they will transition to the parry state.
        {
            stateMachine.ChangeState(playerController.ParryState);
        }
        if (!playerController.PlayerInput.IsShooting) //if the player stops pressing the shot button, then they will return to the idle state.
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
    }

    public void OnExit()
    {
        playerController.WeaponAnimator.SetBool("IsAttacking", false);
    }

    private void BulletSound()
    {
        if (shootCooldown <= 0)
        {
            SoundManager.Instance.CreateSound().WithSoundData(playerController.SoundLibrary.soundData[0]).WithRandomPitch().Play();
            shootCooldown = shootingInterval; //Activate the cooldown.
        }
        else
        {
            shootCooldown -= Time.deltaTime; //Cooldown tick.
        }
    }

    private void ParryCountdown()
    {
        if (canParryTimer > 0f)
        {
            canParryTimer -= Time.deltaTime;
        }
    }
}
