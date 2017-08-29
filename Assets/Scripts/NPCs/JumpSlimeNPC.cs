using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlimeNPC : NPC
{

    private BoxCollider2D box;
    private CapsuleCollider2D cap;
    private CircleCollider2D circ;

    //private CharacterStats cs;

    private const int SLIME_MAX_HEALTH = 1;
    private WeaponStats bodyHitbox;
    private Animator ani;
    private float jumpWaitStart;
    public float secondsToJump = 3f;
    public float dist2Mack;

    private Vector2 newPos;
    private bool setNewPos = false;

    protected override void Start()
    {

        box = gameObject.GetComponent<BoxCollider2D>();
        cap = gameObject.GetComponent<CapsuleCollider2D>();
        circ = gameObject.GetComponent<CircleCollider2D>();

        //cs = gameObject.GetComponent<CharacterStats>();
        ani = gameObject.GetComponent<Animator>();

        jumpWaitStart = Time.time;

        bodyHitbox = gameObject.GetComponent<WeaponStats>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = SLIME_MAX_HEALTH;
        maxSpeed = 5f;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {
        base.Update();
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
                ani.SetBool("jump", true);
                jumpWaitStart = Time.time;
            }
        }
        else
        {
            ani.SetBool("jump", false);
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Slime Walk"))
        {
            box.enabled = true;
            cap.enabled = false;
            circ.enabled = false;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Slime Jump"))
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
                newPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f);
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

        ani.SetBool("grounded", grounded);
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
            velocityX = -5f;
        }
    }

}