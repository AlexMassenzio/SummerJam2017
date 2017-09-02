using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeNPC : NPC
{

    public enum movementState { leftLine, leftCircle, rightLine, rightCircle };
    public enum Phase { firstPhase, secondPhase, finalPhase };
    public movementState state = movementState.leftLine;
    public Phase currentPhase = Phase.firstPhase;
    public float x, y;
    public float eyeSpeed = 2f;
    public float radius = 5f;
    private float timeCounter;

    public Vector2 startPos, leftVertex, rightVertex, targetVec;

    private const int SLIME_MAX_HEALTH = 1;
    private WeaponStats bodyHitbox;
    private PolygonCollider2D pc;
    private SpriteRenderer sre;
    private Animator anim;

    protected override void Start()
    {
        timeCounter = 0;
        startPos = transform.position;
        leftVertex = new Vector2(startPos.x - 5f, startPos.y);
        rightVertex = new Vector2(startPos.x + 5f, startPos.y);

        anim = gameObject.GetComponent<Animator>();
        sre = gameObject.GetComponent<SpriteRenderer>();
        sre.flipX = true;
        bodyHitbox = gameObject.GetComponent<WeaponStats>();
        pc = gameObject.GetComponent<PolygonCollider2D>();

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

        timeCounter += Time.deltaTime * eyeSpeed;

        if (currentPhase == Phase.firstPhase)
        {
            eyeSpeed = 2f;
        }
        else if (currentPhase == Phase.secondPhase)
        {
            eyeSpeed = 2.5f;
        }
        else
        {
            eyeSpeed = 3f;
        }

        switch (state)
        {
            case movementState.leftLine:
                targetVec = new Vector2(leftVertex.x, leftVertex.y + 5f);
                transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y + 0.1f);
                if (transform.position.y >= leftVertex.y + 5)
                {
                    timeCounter = (Mathf.PI / 2) - (Time.deltaTime * eyeSpeed);
                    state = movementState.leftCircle;
                }
                break;
           
            case movementState.leftCircle:
                x = (Mathf.Cos(timeCounter) * radius) + leftVertex.x;
                y = (Mathf.Sin(timeCounter) * radius) + leftVertex.y;
                transform.position = new Vector2(x, y);
                if (Mathf.Abs(transform.position.x - leftVertex.x) < 0.1f 
                    && Mathf.Abs(transform.position.y - leftVertex.y + 5) < 0.1f)
                {
                    state = movementState.rightLine;
                }
                break;

            case movementState.rightLine:
                transform.position = new Vector2(transform.position.x + (0.05f * eyeSpeed), transform.position.y + (0.05f * eyeSpeed));
                if (transform.position.y >= rightVertex.y + 5)
                {
                    timeCounter -= Time.deltaTime * eyeSpeed;
                    state = movementState.rightCircle;
                }
                break;
                
            case movementState.rightCircle:
                x = (-Mathf.Cos(timeCounter) * radius) + rightVertex.x;
                y = (Mathf.Sin(timeCounter) * radius) + rightVertex.y;
                transform.position = new Vector2(x, y);
                if (Mathf.Abs(transform.position.x - rightVertex.x) < 0.1f
                    && Mathf.Abs(transform.position.y - rightVertex.y + 5) < 0.1f)
                {
                    state = movementState.leftLine;
                }
                break;
        }
      
        switch (currentPhase)
        {
            case Phase.firstPhase:
                Debug.Log("In first phase");
                if (cs.health <= 150)
                {
                    //anim.SetBool("secondPhase", true);
                    currentPhase = Phase.secondPhase;
                }
                break;

            case Phase.secondPhase:
                Debug.Log("In second phase");
                if (cs.health <= 75)
                {
                    //anim.SetBool("thirdPhase", true);
                    currentPhase = Phase.finalPhase;
                }
                break;

            case Phase.finalPhase:
                Debug.Log("In third phase");
                break;
        }

        if (cs.hitstunLeft > 0)
        {
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        if (cs.health <= 0)
        {
            pc.enabled = false;
            sr.flipX = false;
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

    protected override void Action()
    {

    }

    protected override void ComputeVelocity()
    {
        /*
        if (cs.hitstunLeft > 0 || cs.dying || cs.dead)
        {
            velocityX = 0;
            velocityY = 0;
        }
        else
        {
            velocityX = -5f;
        }
        */
    }

}