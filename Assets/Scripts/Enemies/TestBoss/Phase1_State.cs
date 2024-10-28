using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1_State : IState
{
    private BossController controller;
    private StateMachine stateMachine;

    private AbstractTurret currentTurret;
    private bool wasFired = false;

    private float timeBetweenAttacks = 2f;
    private float timer;

    public Phase1_State(StateMachine machine, BossController controller) 
    {
        stateMachine = machine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        EnemyHealth.OnDamageReceived += SwitchToNextPhase;
        controller.ShuffleList(controller.Phase1Weapons);
        SwitchCurrentWeapon();
        Debug.Log("Entering Phase 1");
    }

    public void StateUpdate()
    {
        if (timer <= 0)
        {
            HandleCurrentWeapon();
            timer = timeBetweenAttacks;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void HandleCurrentWeapon()
    {
        if (wasFired && !currentTurret.IsShoting)
        {
            SwitchCurrentWeapon();
        }
        else if (!currentTurret.IsShoting)
        {
            currentTurret.Shoot();
            wasFired = true;
        }
    }

    private void SwitchCurrentWeapon()
    {
        if (controller.Phase1Queue.Count > 0)
        {
            currentTurret = controller.Phase1Queue.Dequeue();
            Debug.Log("SwitchWeapon");
            wasFired = false;
        } 
        else
        {
            controller.ShuffleList(controller.Phase1Weapons);
            SwitchCurrentWeapon();
        }
    }

    private void SwitchToNextPhase(float maxHealth, float currentHealth)
    {
        if (currentHealth <= (maxHealth * 0.66f))
        {
            stateMachine.ChangeState(controller.Phase2State);
            Debug.Log("Next Phase");
            EnemyHealth.OnDamageReceived -= SwitchToNextPhase;
        }
    }

    public void OnExit()
    {

    }
}
