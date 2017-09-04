using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlimeNPC : NPC
{
    private GameObject character;
    private BoxCollider2D box;
    private CapsuleCollider2D cap;
    private CircleCollider2D circ;

    private const int SLIME_MAX_HEALTH = 1;
    private Animator anim;
    private WeaponStats bodyHitbox;
    private float jumpWaitStart;
    public float secondsToJump = 3f;
    public float dist2Mack;

    private Vector2 newPos;
    private bool setNewPos = false;

    protected override void Start()
    {

        character = GameObject.FindGameObjectWithTag("Character");
        box = gameObject.GetComponent<BoxCollider2D>();
        cap = gameObject.GetComponent<CapsuleCollider2D>();
        circ = gameObject.GetComponent<CircleCollider2D>();

        jumpWaitStart = Time.time;

        anim = gameObject.GetComponent<Animator>();
        bodyHitbox = gameObject.GetComponent<WeaponStats>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = SLIME_MAX_HEALTH;
        maxSpeed = 2f;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {

        // Basic AI that will reverse direction if running into a wall

        Vector2[] origins = new Vector2[4];
        RaycastHit2D[] hits = new RaycastHit2D[4];
        float xBaseOffset = 1.8f;
        float yBaseOffset = -0.5f;
        float checkDistance = 0.1f;
        Vector2 checkDirection = Vector2.right;

        // If we're facing left
        if (sr.flipX)
        {
            xBaseOffset = -xBaseOffset;
            checkDirection = Vector2.left;
        }

        if (grounded)
        {
            yBaseOffset = -0.5f;
        }
        else
        {
            yBaseOffset = 1f;
        }

        for (int i = 0; i < 4; i++)
        {
            origins[i] = new Vector2(transform.position.x + xBaseOffset, transform.position.y + yBaseOffset);
            yBaseOffset -= 0.5f;
            hits[i] = Physics2D.Raycast(origins[i], checkDirection, checkDistance);
            Debug.DrawLine(origins[i], new Vector2(origins[i].x + checkDistance, origins[i].y));
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Wall")
            {
                Debug.Log("Detected wall");
                sr.flipX = !sr.flipX;
                cs.maxSpeed *= -1;
                break;
            }
            // If none of the triggers hit anything
            else if (i == 5)
            {
                Debug.Log("Not hitting anything");
            }
        }

        base.Update();

        if (cs.hitstunLeft > 0)
        {
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        if (cs.health <= 0 || Vector2.Distance(character.transform.position, gameObject.transform.position) > 100)
        {
            box.enabled = false;
            cap.enabled = false;
            circ.enabled = false;
            sr.flipX = false;
            anim.SetBool("dead", true);
        }

    }

    protected override void FixedUpdate()
    {
        GameObject mack = GameObject.FindGameObjectWithTag("Player");
        dist2Mack = Vector2.Distance(mack.transform.position, gameObject.transform.position);
        
        if (Time.time - jumpWaitStart > secondsToJump && dist2Mack < 5)
        {
            if (grounded)
            {
                velocity = new Vector2(velocity.x, Vector2.up.y * 20);
                anim.SetBool("jump", true);
                jumpWaitStart = Time.time;
            }
        }
        else
        {
            anim.SetBool("jump", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slime Walk"))
        {
            box.enabled = true;
            cap.enabled = false;
            circ.enabled = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slime Jump"))
        {
            box.enabled = false;
            cap.enabled = true;
            circ.enabled = false;
        }
        else
        {
            box.enabled = false;
            cap.enabled = false;
            circ.enabled = true;
        }

        if (sr.flipX)
        {
            cap.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y);
        }
        else
        {
            cap.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);
        }

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = velocityX;

        bool oldGrounded = grounded;
        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, 'x');

        move = Vector2.up * deltaPosition.y;

        Movement(move, 'y');

        if (oldGrounded == false && grounded == true)
        {
            // If we are landing from air, teleport up a little
            if (circ.enabled)
            {
                Debug.Log("Landing from air");
                newPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2f);
                setNewPos = true;
            }
            else if (cap.enabled)
            {
                Debug.Log("Landing from jump");
                newPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f);
                setNewPos = true;
            }
        }

        if (setNewPos)
        {
            gameObject.transform.position = newPos;
            setNewPos = false;
        }

        anim.SetBool("grounded", grounded);
    }

    protected override void Action()
    {

    }

    protected override void ComputeVelocity()
    {
        // Just move to the left at a constant speed
        if (cs.hitstunLeft > 0 || cs.dying || cs.dead)
        {
            velocityX = 0;
        }
        else
        {
            velocityX = -0f;
        }
    }

}