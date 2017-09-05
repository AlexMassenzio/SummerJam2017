using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MackAttack : MonoBehaviour {

    public int damage = 2;
    public float hitstunDuration = 0.5f;

    public bool attacking = false;
    public bool delayEnabled = false;
    public bool timeEnabled = false;

    public float attackDelayMax;
    public float attackDelayLeft = 0;
    public float attackTimeMax;
    public float attackTimeLeft = 0;

    public Collider2D attackTrigger;
    private PlayerManager pm;
    private WeaponStats ws;

    private float harpoonSoundTimer;

    private const float MACK_ATTACK_DELAY = 0.3f;
    private const float MACK_ATTACK_TIME = 0.5f;
    private const float HARPOON_DELAY = 0.3f;
    private const float HARPOON_TIME = 0.425f;
    private const float BETTER_HARPOON_DELAY = 0.325f;
    private const float BETTER_HARPOON_TIME = 0.425f;
    private const float BEST_HARPOON_DELAY = 0.25f;
    private const float BEST_HARPOON_TIME = 0.4f;

    private void Start()
    {

        // Default to having the hitbox disabled
        attackTrigger = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
        attackTrigger.enabled = false;

        pm = gameObject.GetComponentInChildren<PlayerManager>();
        ws = gameObject.GetComponentInChildren<WeaponStats>();

        ws.damage = this.damage;
        ws.hitstunDuration = this.hitstunDuration;
    }

    private void Update()
    {

        harpoonSoundTimer -= Time.deltaTime;

        if (pm.hasHarpoon)
        {
            attackTrigger = transform.GetChild(2).GetComponent<BoxCollider2D>();
            attackDelayMax = HARPOON_DELAY;
            attackTimeMax = HARPOON_TIME;
        }
        else if (pm.hasBetterHarpoon)
        {
            attackTrigger = transform.GetChild(3).GetComponent<BoxCollider2D>();
            attackDelayMax = BETTER_HARPOON_DELAY;
            attackTimeMax = BETTER_HARPOON_TIME;
        }
        else if (pm.hasBestHarpoon)
        {
            attackTrigger = transform.GetChild(4).GetComponent<BoxCollider2D>();
            attackDelayMax = BEST_HARPOON_DELAY;
            attackTimeMax = BEST_HARPOON_TIME;
        }
        // If we only have Mack Attack
        else
        {
            attackTrigger = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
            attackDelayMax = MACK_ATTACK_DELAY;
            attackTimeMax = MACK_ATTACK_TIME;
        }

        // GetMouseButtonDown(0) is left click, 1 is right, and 2 is middle
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            if (harpoonSoundTimer <= 0)
            {
                if (pm.hasHarpoon || pm.hasBetterHarpoon)
                {
                    SoundManager.PlaySound("harpoonSound");
                }
                else if (pm.hasBestHarpoon)
                {
                    SoundManager.PlaySound("bestHarpoonSound");
                }
                else
                {
                    SoundManager.PlaySound("mackAttackSound");
                }
                harpoonSoundTimer = 0.75f;
            }
            attacking = true;
            delayEnabled = true;
            attackDelayLeft = attackDelayMax;
        }

        if (attacking)
        {
            if (delayEnabled)
            {
                if (attackDelayLeft > 0)
                {
                    attackDelayLeft -= Time.deltaTime;
                }
                else
                {
                    delayEnabled = false;
                    timeEnabled = true;
                    attackTimeLeft = attackTimeMax;
                    attackTrigger.enabled = true;
                }
            }
            else if (timeEnabled)
            {
                if (attackTimeLeft > 0)
                {
                    attackTimeLeft -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    timeEnabled = false;
                    attackTrigger.enabled = false;
                }
            }
        }
    }
}
