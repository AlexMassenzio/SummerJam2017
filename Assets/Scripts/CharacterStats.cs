using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private PlayerController pc;

    public int health;
    public float maxSpeed;
    public float currentSpeed;
    public float crouchSpeed;
    public float jumpTakeOffSpeed;
    public float stamina;
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

    public float invincibilityMax = 2f;
    public float invincibilityLeft = 0;

    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            pc = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
        }
    }

    public void Injure(int damage)
    {
        health -= damage;
        invincibilityLeft = invincibilityMax;
    }

    public void Hitstun(float hitstunDuration)
    {
        hitstunLeft = hitstunDuration;
    }
    
    public void Knockback(Vector2 knockback)
    {
        //SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        pc.velocity = knockback;
    }
    
    public void Update()
    {

        if (invincibilityLeft > 0)
        {
            invincibilityLeft -= Time.deltaTime;
        }

        if (hitstunLeft > 0)
        {
            hitstunLeft -= Time.deltaTime;
        }

        // Bye Bye
        if (health <= 0)
        {
            if (!dying && !dead)
            {
                dying = true;
                dyingTimeLeft = dyingTimeMax;
            }
            else if (dying && !dead && dyingTimeLeft > 0)
            {
                dyingTimeLeft -= Time.deltaTime;
            }
            else if (dying && !dead && dyingTimeLeft <= 0)
            {
                dead = true;
                deadTimeLeft = deadTimeMax;
            }
            else if (dead && deadTimeLeft > 0)
            {
                deadTimeLeft -= Time.deltaTime;
            }
            else if (dead && deadTimeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
        }

    }
}