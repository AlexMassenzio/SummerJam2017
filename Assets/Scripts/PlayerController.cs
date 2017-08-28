using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{

    private MackAttack ma;
    private Inventory inv;

    public GameObject anchor;
    public GameObject character;
    public GameObject knife;

    public bool crouching = false;

    private float weaponHorizontalPos;

    private float knifeCrouchingHeight;
    private float knifeStandingHeight;
    private float anchorCrouchingHeight;
    private float anchorStandingHeight;

    private Vector2 knifeSpawnPos;
    private Vector2 anchorSpawnPos;

    protected override void Start()
    {
        base.Start();

        ma = gameObject.GetComponent<MackAttack>();
        character = transform.GetChild(0).gameObject;
        inv = gameObject.GetComponentInChildren<Inventory>();
        cs = gameObject.GetComponentInChildren<CharacterStats>();

		cs.maxHealth = 25;
        cs.health = cs.maxHealth;
        cs.maxSpeed = 10f;
        cs.crouchSpeed = cs.maxSpeed / 3;
        cs.jumpTakeOffSpeed = 25f;
		cs.maxStamina = 100;
        cs.stamina = cs.maxStamina;

        if (gameObject.name == "Mack")
        {
            cs.currentSpeed = 10;
        }

        weaponHorizontalPos = 1.5f;

    }

    private void OnEnable()
    {
        rb2d = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        knifeCrouchingHeight = character.transform.position.y + 0.5f;
        anchorCrouchingHeight = character.transform.position.y + 0.5f;
        knifeStandingHeight = character.transform.position.y + 0.8f;
        anchorStandingHeight = character.transform.position.y + 0.8f;

        if (crouching)
        {
            // If facing left
            if (sr.flipX)
            {
                knifeSpawnPos = new Vector2(character.transform.position.x - weaponHorizontalPos, knifeCrouchingHeight);
                anchorSpawnPos = new Vector2(character.transform.position.x - weaponHorizontalPos, anchorCrouchingHeight);
            }
            else
            {
                knifeSpawnPos = new Vector2(character.transform.position.x + weaponHorizontalPos, knifeCrouchingHeight);
                anchorSpawnPos = new Vector2(character.transform.position.x + weaponHorizontalPos, anchorCrouchingHeight);
            }
        }
        else
        {
            if (sr.flipX)
            {
                knifeSpawnPos = new Vector2(character.transform.position.x - weaponHorizontalPos, knifeStandingHeight);
                anchorSpawnPos = new Vector2(character.transform.position.x - weaponHorizontalPos, anchorStandingHeight);
            }
            else
            {
                knifeSpawnPos = new Vector2(character.transform.position.x + weaponHorizontalPos, knifeStandingHeight);
                anchorSpawnPos = new Vector2(character.transform.position.x + weaponHorizontalPos, anchorStandingHeight);
            }
        }

        if (cs.health <= 0)
        {
            EventManager.TriggerEvent("MackDeath");
        }

        // Use Weapon
        if (Input.GetMouseButtonDown(1) && inv.haveWeapon && cs.hitstunLeft <= 0 && cs.stamina > 0)
        {
            if (inv.cooldownLeft <= 0)
            {
                if (inv.weaponName == "Anchor")
                {
                    Instantiate(anchor, anchorSpawnPos, new Quaternion());
                    WeaponStats ws = anchor.GetComponent<WeaponStats>();
                    cs.stamina -= ws.staminaCost;
                }
                else if (inv.weaponName == "Knife")
                {
                    Instantiate(knife, knifeSpawnPos, new Quaternion());
                    WeaponStats ws = knife.GetComponent<WeaponStats>();
                    cs.stamina -= ws.staminaCost;
                }
            }
        }
    }

    protected override void ComputeVelocity()
    {

        Vector2 move = Vector2.zero;

        if (cs.hitstunLeft <= 0 && inv.useStunLeft <= 0 || (inv.useStunLeft > 0 && !grounded))
        {
            if (!grounded || !ma.attacking)
            {
                move.x = Input.GetAxisRaw("Horizontal");
            }

            // Crouching
            if (Input.GetAxisRaw("Vertical") == -1 && (grounded || crouching))
            {
                crouching = true;
                cs.currentSpeed = cs.crouchSpeed;
            }
            else
            {
                // Code so Mack can't stop crouching if something is above his head
                GameObject ch = transform.GetChild(0).gameObject;
                Vector2[] origins = new Vector2[5];
                RaycastHit2D[] hits = new RaycastHit2D[5];
                float xBaseOffset = -0.65f;
                float checkDistance = 0.409f;
                for (int i = 0; i < 5; i++)
                {
                    origins[i] = new Vector2(ch.transform.position.x + xBaseOffset, ch.transform.position.y + 1.19f);
                    xBaseOffset += 0.325f;
                    hits[i] = Physics2D.Raycast(origins[i], Vector2.up, checkDistance);
                    if (hits[i].collider != null)
                    {
                        break;
                    }
                    else if (i == 4)
                    {
                        crouching = false;
                        cs.currentSpeed = cs.maxSpeed;
                    }
                }
            }

            // Jumping
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocityY = cs.jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocityY > 0)
                {
                    velocityY = velocity.y * 0.5f;
                }
            }
        }
        velocityX = move.x * cs.currentSpeed;
    }

    public bool isGrounded()
    {
        return grounded;
    }

}