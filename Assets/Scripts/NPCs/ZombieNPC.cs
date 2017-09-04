using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNPC : NPC
{

    private const int SLIME_MAX_HEALTH = 1;
    private CharacterStats css;
    private WeaponStats bodyHitbox;
    private CapsuleCollider2D cc;
    private SpriteRenderer sre;
    private Animator anim;
    private GameObject character;

    protected override void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        anim = gameObject.GetComponent<Animator>();
        cc = gameObject.GetComponent<CapsuleCollider2D>();
        sre = gameObject.GetComponent<SpriteRenderer>();
        sre.flipX = true;
        css = gameObject.GetComponent<CharacterStats>();
        bodyHitbox = gameObject.GetComponent<WeaponStats>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = SLIME_MAX_HEALTH;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {

        // Basic AI that will reverse direction if running into a wall

        Vector2[] origins = new Vector2[6];
        RaycastHit2D[] hits = new RaycastHit2D[6];
        float xBaseOffset = 1.5f;
        float yBaseOffset = 1f;
        float checkDistance = 0.2f;
        Vector2 checkDirection = Vector2.right;

        // If we're facing left
        if (sr.flipX)
        {
            xBaseOffset = -xBaseOffset;
            checkDirection = Vector2.left;
        }

        for (int i = 0; i < 6; i++)
        {
            origins[i] = new Vector2(transform.position.x + xBaseOffset, transform.position.y + yBaseOffset);
            yBaseOffset -= 0.5f;
            hits[i] = Physics2D.Raycast(origins[i], checkDirection, checkDistance);
            Debug.DrawLine(origins[i], new Vector2(origins[i].x + checkDistance, origins[i].y));
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Wall" && css.health > 0)
            {
                Debug.Log("Detected wall");
                sr.flipX = !sr.flipX;
                css.maxSpeed *= -1;
                break;
            }
            // If none of the triggers hit anything
            else if (i == 5)
            {
                Debug.Log("Not hitting anything");
            }
        }

        base.Update();

        if (css.hitstunLeft > 0)
        {
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        if (css.health <= 0 || Vector2.Distance(character.transform.position, gameObject.transform.position) > 100)
        {
            cc.enabled = false;
            velocity = new Vector2();
            velocityX = 0;
            velocityY = 0;
            anim.SetBool("dead", true);
        }
    }

    protected override void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = velocityX;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, 'x');

        move = Vector2.up * deltaPosition.y;

        Movement(move, 'y');
    }

    protected override void ComputeVelocity()
    {
        if (css.hitstunLeft > 0 || css.dying || css.dead)
        {
            velocityX = 0;
            velocityY = 0;
        }
        else
        {
            velocityX = -css.maxSpeed;
        }
    }

}