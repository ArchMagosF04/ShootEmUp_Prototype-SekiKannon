using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3_State : IState
{
    private BossController controller;
    private StateMachine stateMachine;

    private AbstractTurret currentTurret;
    private bool wasFired = false;

    private float timeBetweenAttacks = 1.5f;
    private float timer;

    public Phase3_State(StateMachine machine, BossController controller)
    {
        stateMachine = machine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        EnemyHealth.OnEnemyDeath += SwitchToNextPhase;
        controller.ShuffleList(controller.Phase3Weapons);
        SwitchCurrentWeapon();
        Debug.Log("Entering Phase 3");
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
            controller.ShuffleList(controller.Phase3Weapons);
            SwitchCurrentWeapon();
        }
    }

    private void SwitchToNextPhase()
    {
        stateMachine.ChangeState(controller.BossDeathState);
        Debug.Log("Next Phase");
        EnemyHealth.OnEnemyDeath -= SwitchToNextPhase;
    }

    public void OnExit()
    {

    }
}
