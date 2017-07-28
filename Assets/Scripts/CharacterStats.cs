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
    public float cooldown;

    public float hitstunDuration = 0f;

    public bool dead = false;
    public bool dying = false;
    
    // Dying animation duration
    public float dyingTimeMax;
    public float dyingTimeLeft = 0f;

    // Time laying there dead
    public float deadTimeMax;
    public float deadTimeLeft = 0f;

    /*
    public CharacterStats(int health, DamageInfo myDI, float maxSpeed, 
                          float crouchSpeed, float jumpTakeOffSpeed, 
                          float cooldown, int stamina, float dyingTime,
                          float deadTime)
    {
        this.myDamageInfo = myDI;
        this.health = health;
        this.maxSpeed = maxSpeed;
        this.crouchSpeed = crouchSpeed;
        this.jumpTakeOffSpeed = jumpTakeOffSpeed;
        this.stamina = stamina;
        this.dyingTimeMax = dyingTime;
        this.deadTimeMax = deadTime;
    }
    */

    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }

    public void Injure(DamageInfo di)
    {
        health -= di.damageModifier;
    }

    public void Hitstun(float hitstunDuration)
    {
        
    }

    public void Knockback()
    {

    }

    public void Update()
    {

        if (hitstunDuration > 0)
        {
            // Disable controls and hurtboxes
            if (gameObject.tag == "Player") 
            {
                // TODO: disable controls and hurtboxes
            }
            // Break its spirit
            else if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<SlimeNPC>().actionable = false;
            }

            hitstunDuration -= Time.deltaTime;
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
    }
}