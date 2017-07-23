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
    public float cooldown;
    
    public bool dead = false;
    public bool dying = false;
    
    // Dying animation duration
    public float dyingTimeMax = 2.5f;
    public float dyingTimeLeft = 0f;

    // Time laying there dead
    public float deadTimeMax = 1f;
    public float deadTimeLeft = 0f;

    public CharacterStats(int health, DamageInfo myDI, float maxSpeed, float crouchSpeed, float jumpTakeOffSpeed, float cooldown)
    {
        this.myDamageInfo = myDI;
        this.health = health;
        this.maxSpeed = maxSpeed;
        this.crouchSpeed = crouchSpeed;
        this.jumpTakeOffSpeed = jumpTakeOffSpeed;
        this.cooldown = cooldown;
    }

    public void Start()
    {
        dyingTimeMax = 2.5f;
        deadTimeMax = 1f;
    }

    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }

    public void Injure(DamageInfo di)
    {
        health -= di.damageModifier;
    }

    public void Update()
    {
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
    }
}
