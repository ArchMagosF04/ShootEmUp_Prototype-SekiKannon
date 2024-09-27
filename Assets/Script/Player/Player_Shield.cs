using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    [SerializeField] private float maxOverloadLevel = 10f; 
    private float currentOverloadLevel = 0f;

    [SerializeField] private float rechargeRate = 1f;
    private float currentRechargeRate;

    [SerializeField] private float startRegenDelay = 2f;
    private float regenDelay;

    [SerializeField, Range(0.25f, 1f)] private float parryDamageReduction = 0.75f;

    public bool isParryActive = false;
    private bool hasBeenResentlyDamaged = false;
    public bool shieldBroken = false;

    [SerializeField] private HealthBar shieldBar;

    private void Start()
    {
        currentOverloadLevel = 0f;
        currentRechargeRate = rechargeRate;
        regenDelay = startRegenDelay;
        shieldBar.SetMaxHealth(maxOverloadLevel, currentOverloadLevel);
    }

    private void Update()
    {
        if(!hasBeenResentlyDamaged && currentOverloadLevel > 0)
        {
            RegenShield();
        }
        else if(!shieldBroken)
        {
            RegenDelay();
        }
    }

    private void RegenDelay()
    {
        if(regenDelay <= 0)
        {
            hasBeenResentlyDamaged=false;
            regenDelay = startRegenDelay;
        }else
        {
            regenDelay -=Time.deltaTime;
        }
    }

    private void RegenShield()
    {
        if (currentRechargeRate <= 0)
        {
            currentOverloadLevel--;
            if (currentOverloadLevel < 0)
            {
                currentOverloadLevel = 0;
            }
            shieldBar.SetHealth(currentOverloadLevel);
            currentRechargeRate = rechargeRate;
        }
        else
        {
            currentRechargeRate -= Time.deltaTime;
        }      
    }

    public void TakeDamage(float damageReceived)
    {
        currentOverloadLevel += damageReceived;
        hasBeenResentlyDamaged = true;

        shieldBar.SetHealth(currentOverloadLevel);

        if (currentOverloadLevel >= maxOverloadLevel)
        {
            currentOverloadLevel = maxOverloadLevel;
            shieldBroken = true;
        }
    }

    public void TakeSafeDamage(float damageReceived)
    {
        float temp = currentOverloadLevel;
        hasBeenResentlyDamaged = true;

        if (temp + Mathf.FloorToInt(damageReceived * parryDamageReduction) < maxOverloadLevel)
        {
            currentOverloadLevel += Mathf.FloorToInt(damageReceived * parryDamageReduction);
        }
        shieldBar.SetHealth(currentOverloadLevel);
    }

    public void ToggleParry()
    {
        if(!isParryActive)
        {
            isParryActive=true;
        }else
        {
            isParryActive=false;
        }
    }

    public void RestoreShield()
    {
        regenDelay = startRegenDelay;
        hasBeenResentlyDamaged = true;

        int temp = Mathf.FloorToInt(maxOverloadLevel / 2);
        currentOverloadLevel -= temp;
        shieldBar.SetHealth(currentOverloadLevel);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IParryEffect interceptedBullet = collision.GetComponent<IParryEffect>();

        if (interceptedBullet != null)
        {
            if(isParryActive)
            {
                interceptedBullet.OnParryEffect(this);
            }else
            {
                interceptedBullet.OnBlockEffect(this);
            }
        }
    }
}
