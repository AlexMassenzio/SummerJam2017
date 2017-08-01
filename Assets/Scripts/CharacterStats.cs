using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public DamageInfo myDamageInfo;
    public int health;
    public float maxSpeed;
    public float currentSpeed;
    public float crouchSpeed;
    public float jumpTakeOffSpeed;
    public int stamina;
    public float cooldownLeft;
    public float hitstunLeft;

    public bool dead = false;
    public bool dying = false;
    public bool usingWeapon = false;

    // Dying animation duration
    public float dyingTimeMax;
    public float dyingTimeLeft = 0f;

    // Time laying there dead
    public float deadTimeMax;
    public float deadTimeLeft = 0f;
    
    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }

    public void Injure(DamageInfo di)
    {
        health -= di.damageModifier;
    }

    public void Hitstun(DamageInfo di)
    {
        hitstunLeft = di.hitstunDuration;
    }

    public void Knockback()
    {

    }

    public void Update()
    {

        if (hitstunLeft > 0)
        {
            hitstunLeft -= Time.deltaTime;
        }

        // Bye Bye
        if (health <= 0)
        {
            if (!dying && !dead)
            {
                Debug.Log("started dying");
                dying = true;
                dyingTimeLeft = dyingTimeMax;
                Debug.Log("dyingTimeLeft: " + dyingTimeLeft);
            }
            else if (dying && !dead && dyingTimeLeft > 0)
            {
                Debug.Log("dying");
                dyingTimeLeft -= Time.deltaTime;
            }
            else if (dying && !dead && dyingTimeLeft <= 0)
            {
                Debug.Log("started death");
                dead = true;
                deadTimeLeft = deadTimeMax;
            }
            else if (dead && deadTimeLeft > 0)
            {
                Debug.Log("being dead");
                deadTimeLeft -= Time.deltaTime;
            }
            else if (dead && deadTimeLeft <= 0)
            {
                Debug.Log("finished death");
                Destroy(gameObject);
            }
        }
        
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
        }

    }
}