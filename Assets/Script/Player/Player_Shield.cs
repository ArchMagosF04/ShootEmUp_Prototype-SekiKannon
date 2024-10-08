using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    [SerializeField] private int maxOverloadLevel = 10; //Max value of the shield durability.
    private int currentOverloadLevel = 0;

    [SerializeField] private float rechargeRate = 1f; //Rate in at which a tick of shield regeneration occurs.
    private float currentRechargeRate;

    [SerializeField] private float startRegenDelay = 2f; //Time in seconds before the shield starts to regenerate after being damaged.
    private float regenDelay;

    [SerializeField, Range(0.25f, 1f)] private float parryDamageReduction = 0.75f; //Porcentage of damage that will be taken when parrying where 1f == 100%.

    public bool isParryActive = false; 
    private bool hasBeenResentlyDamaged = false;
    public bool shieldBroken = false;

    [SerializeField] private HealthBar shieldBar;

    private Collider2D circleCollider;
    private SpriteRenderer sprite;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentOverloadLevel = 0;
        currentRechargeRate = rechargeRate;
        regenDelay = startRegenDelay;
        shieldBar.SetMaxHealth(maxOverloadLevel, currentOverloadLevel);

        ToggleShield(false); //The shield begins off.
    }

    private void Update()
    {
        if(!hasBeenResentlyDamaged && currentOverloadLevel > 0) //If the Regen Delay is over and the bar isn't empty, then it will regenerate the shield.
        {
            RegenShield();
        }
        else if(!shieldBroken) //When being recently damage it will start to count down the time to start the regeneration. This will not occur if the shield is broken.
        {
            RegenDelay();
        }
    }

    private void RegenDelay() //When the timer ends the shield will be allowed to start to regenerate.
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

    private void RegenShield() //Every tick, determined by the recharge rate, it will regenerate the shield a little.
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

    public void TakeDamage(int damageReceived) //Receives the damage value to be dealt to the shield.
    {
        currentOverloadLevel += damageReceived;
        hasBeenResentlyDamaged = true;
        regenDelay = startRegenDelay;

        shieldBar.SetHealth(currentOverloadLevel);

        if (currentOverloadLevel >= maxOverloadLevel) //If the bar fully fills the shield is broken.
        {
            currentOverloadLevel = maxOverloadLevel;
            shieldBroken = true;
        }
    }

    public void TakeSafeDamage(int damageReceived) //Receives the damage value to be dealt to the shield, but prevents the bar to fully reach its limit.
    {
        float temp = currentOverloadLevel;
        hasBeenResentlyDamaged = true;
        regenDelay = startRegenDelay;

        if (temp + Mathf.FloorToInt(damageReceived * parryDamageReduction) < maxOverloadLevel)
        {
            currentOverloadLevel += Mathf.FloorToInt(damageReceived * parryDamageReduction);
        }
        else
        {
            currentOverloadLevel = Mathf.FloorToInt(maxOverloadLevel * 0.95f);
        }

        shieldBar.SetHealth(currentOverloadLevel);
    }

    public void ToggleParry() //Toggles parry mode on & off.
    {
        isParryActive = !isParryActive;
    }

    public void RestoreShield() //Instantly heals the shield by half of its maximum value.
    {
        regenDelay = startRegenDelay;
        hasBeenResentlyDamaged = true;

        int temp = Mathf.FloorToInt(maxOverloadLevel / 2);
        currentOverloadLevel -= temp;
        shieldBar.SetHealth(currentOverloadLevel);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IParryEffect interceptedBullet = collision.GetComponent<IParryEffect>(); //Checks if the object collided with implements the interface.

        if (interceptedBullet != null)
        {
            if(isParryActive) //If in parry mode, it will use the bullets effects from onParry, if on normal mode, it will ise the bullets effects from onBlock.
            {
                interceptedBullet.OnParryEffect(this);
            }else
            {
                interceptedBullet.OnBlockEffect(this);
            }
        }
    }

    public void ToggleShield(bool shouldActivate) //Turns the shield on & off.
    {
        circleCollider.enabled = shouldActivate;
        sprite.enabled = shouldActivate;
    }
}
