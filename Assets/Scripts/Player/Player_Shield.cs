using System;
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
    private bool shieldBroken = false;
    public bool ShieldBroken => shieldBroken;

    private Collider2D circleCollider;
    private SpriteRenderer shieldSprite;
    [SerializeField] private SpriteRenderer staticSprite;

    [SerializeField] private Gradient shieldGradient;
    private float target = 0f;

    public static event Action<float, float> OnDamageReceived;

    private Color parryColor;
    private Color blockColor;

    private Coroutine colorChange;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
        shieldSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentOverloadLevel = 0;
        currentRechargeRate = rechargeRate;
        regenDelay = startRegenDelay;

        parryColor = Color.cyan;
        blockColor = shieldGradient.Evaluate(target);

        ToggleShield(false); //The shield begins off.
        BreakShield(false);
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
            StartCoroutine(GradualChange());
            if (currentOverloadLevel < 0)
            {
                currentOverloadLevel = 0;
            }
            OnDamageReceived?.Invoke(maxOverloadLevel, currentOverloadLevel);

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
        StartCoroutine(GradualChange());
        hasBeenResentlyDamaged = true;
        regenDelay = startRegenDelay;

        OnDamageReceived?.Invoke(maxOverloadLevel, currentOverloadLevel);

        if (currentOverloadLevel >= maxOverloadLevel) //If the bar fully fills the shield is broken.
        {
            currentOverloadLevel = maxOverloadLevel;
            BreakShield(true);
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
        StartCoroutine(GradualChange());

        OnDamageReceived?.Invoke(maxOverloadLevel, currentOverloadLevel);
    }

    public void ToggleParry(bool isActive) //Toggles parry mode on & off.
    {
        isParryActive = isActive;

        if (isParryActive)
        {
            parryColor.a = 0.4f;
            shieldSprite.color = parryColor;
        }
        else
        {
            blockColor.a = 0.4f;
            shieldSprite.color = blockColor; 
        }
    }

    public void RestoreShield() //Instantly heals the shield by half of its maximum value.
    {
        regenDelay = startRegenDelay;
        hasBeenResentlyDamaged = true;

        int temp = Mathf.FloorToInt(maxOverloadLevel / 2);
        currentOverloadLevel -= temp;

        if (colorChange != null)
        {
            StopCoroutine(colorChange);
        }

        colorChange = StartCoroutine(GradualChange());
        OnDamageReceived?.Invoke(maxOverloadLevel, currentOverloadLevel);
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
        shieldSprite.enabled = shouldActivate;
    }

    public void BreakShield(bool shouldActivate)
    {
        shieldBroken = shouldActivate;
        staticSprite.enabled = shouldActivate;
    }

    private IEnumerator GradualChange() //Makes the change smooth and creates the effect to fill from the center by using two mirrored bars. Also changes the color the more its filled.
    {
        float tempCurrent = (float)currentOverloadLevel;
        float tempMax = (float)maxOverloadLevel;

        target = tempCurrent / tempMax;

        Color currentColor = blockColor;
        Color newColor = shieldGradient.Evaluate(target);

        float elapsedTime = 0f;
        float timeToChange = 0.25f;

        while (elapsedTime < timeToChange)
        {
            elapsedTime += Time.deltaTime;

            blockColor = Color.Lerp(currentColor, newColor, (elapsedTime / timeToChange));

            if (!isParryActive) 
            {
                shieldSprite.color = blockColor;
            }

            yield return null;
        }
    }
}
