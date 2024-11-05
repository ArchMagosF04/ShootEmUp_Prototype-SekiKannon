using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2_State : IState
{
    private BossController controller;
    private StateMachine stateMachine;

    private AbstractTurret currentTurret;
    private bool wasFired = false;

    private float timeBetweenAttacks = 1.75f;
    private float timer;

    public Phase2_State(StateMachine machine, BossController controller)
    {
        stateMachine = machine;
        this.controller = controller;
    }

    public void OnEnter()
    {
        timer = 1.5f;
        controller.WeaponsQueue.Clear();
        BossHealth.OnDamageReceived += SwitchToNextPhase;
        controller.ShuffleList(controller.Phase2Weapons);
        SwitchCurrentWeapon();
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
        if (controller.WeaponsQueue.Count > 0)
        {
            currentTurret = controller.WeaponsQueue.Dequeue();
            wasFired = false;
        }
        else
        {
            controller.ShuffleList(controller.Phase2Weapons);
            SwitchCurrentWeapon();
        }
    }

    public void SwitchToNextPhase(float maxHealth, float currentHealth)
    {
        if (currentHealth <= (maxHealth * 0.33f))
        {
            BossHealth.OnDamageReceived -= SwitchToNextPhase;
            stateMachine.ChangeState(controller.Phase3State);
        }
    }

    public void OnExit()
    {

    }
}
