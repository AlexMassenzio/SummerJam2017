using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeNPC : NPC
{
    public GameObject door;
    private bool dyingSoundPlayed;
    public float dieSoundTimer, dyingSoundTimer;
    public enum movementState { leftLine, leftCircle, rightLine, rightCircle, stop, funeralMarch };
    public enum Phase { firstPhase, secondPhase, finalPhase };
    public movementState state = movementState.leftLine;
    public movementState prevState = movementState.leftLine;
    public Phase currentPhase = Phase.firstPhase;
    public float x, y;
    public float eyeSpeed = 2f;
    public float radius = 5f;
    public float attackTimer, attackThreshold;
    private float movementCounter, projectileTimer, projectileThreshold;
    public GameObject projectile;
    public Vector2 pathDir;

    public Vector2 startPos, leftVertex, rightVertex, targetVec;

    private const int SLIME_MAX_HEALTH = 1;
    private WeaponStats bodyHitbox;
    private PolygonCollider2D pc;
    private SpriteRenderer sre;
    private Animator anim;

    protected override void Start()
    {
        
        dyingSoundPlayed = false;
        dyingSoundTimer = 3;
        movementCounter = 0;
        projectileTimer = 0;
        startPos = transform.position;
        leftVertex = new Vector2(startPos.x - 5f, startPos.y);
        rightVertex = new Vector2(startPos.x + 5f, startPos.y);

        anim = gameObject.GetComponent<Animator>();
        sre = gameObject.GetComponent<SpriteRenderer>();
        bodyHitbox = gameObject.GetComponent<WeaponStats>();
        pc = gameObject.GetComponent<PolygonCollider2D>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = SLIME_MAX_HEALTH;
        maxSpeed = 5f;

        attackThreshold = Random.value * 10 + 5;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {
        base.Update();
        dyingSoundTimer += Time.deltaTime;
        dieSoundTimer -= Time.deltaTime;
        attackTimer += Time.deltaTime;
        projectileTimer += Time.deltaTime;
        movementCounter += Time.deltaTime * eyeSpeed;

        if (cs.health > 0)
        {
            if (currentPhase == Phase.firstPhase)
            {
                eyeSpeed = 2f;
            }
            else if (currentPhase == Phase.secondPhase)
            {
                projectileThreshold = Random.value * 5f * 60;
                eyeSpeed = 3f;
                if (projectileTimer > projectileThreshold)
                {
                    projectileTimer = 0;
                    Instantiate(projectile, transform.position, new Quaternion());
                }
            }
            else
            {
                projectileThreshold = Random.value * 2f * 60;
                eyeSpeed = 4f;
                if (projectileTimer > projectileThreshold)
                {
                    projectileTimer = 0;
                    Instantiate(projectile, transform.position, new Quaternion());
                }
                if (attackTimer > attackThreshold)
                {
                    prevState = state;
                    state = movementState.stop;
                    anim.SetBool("attack", true);
                    attackTimer = 0;
                    attackThreshold = Random.value * 10 + 5;
                }
                else
                {
                    anim.SetBool("attack", false);
                }
            }
        }

        switch (state)
        {
            case movementState.leftLine:
                targetVec = new Vector2(leftVertex.x, leftVertex.y + 5f);
                transform.position = new Vector2(transform.position.x - (0.05f * eyeSpeed), transform.position.y + (0.05f * eyeSpeed));
                if (transform.position.y >= leftVertex.y + 5)
                {
                    movementCounter = (Mathf.PI / 2) - (Time.deltaTime * eyeSpeed);
                    prevState = state;
                    state = movementState.leftCircle;
                }
                break;
           
            case movementState.leftCircle:
                x = (Mathf.Cos(movementCounter) * radius) + leftVertex.x;
                y = (Mathf.Sin(movementCounter) * radius) + leftVertex.y;
                transform.position = new Vector2(x, y);
                if (Mathf.Abs(transform.position.x - leftVertex.x) < 0.1f 
                    && Mathf.Abs(transform.position.y - leftVertex.y + 5) < 0.1f)
                {
                    prevState = state;
                    state = movementState.rightLine;
                }
                break;

            case movementState.rightLine:
                transform.position = new Vector2(transform.position.x + (0.05f * eyeSpeed), transform.position.y + (0.05f * eyeSpeed));
                if (transform.position.y >= rightVertex.y + 5)
                {
                    movementCounter -= Time.deltaTime * eyeSpeed;
                    prevState = state;
                    state = movementState.rightCircle;
                }
                break;
                
            case movementState.rightCircle:
                x = (-Mathf.Cos(movementCounter) * radius) + rightVertex.x;
                y = (Mathf.Sin(movementCounter) * radius) + rightVertex.y;
                transform.position = new Vector2(x, y);
                if (Mathf.Abs(transform.position.x - rightVertex.x) < 0.1f
                    && Mathf.Abs(transform.position.y - rightVertex.y + 5) < 0.1f)
                {
                    prevState = state;
                    state = movementState.leftLine;
                }
                break;

            case movementState.stop:
                if (cs.health <= 0)
                {
                    if (dieSoundTimer <= 0 && dyingSoundTimer < 2)
                    {
                        SoundManager.PlaySound("dieSound");
                        dieSoundTimer = 0.5f;
                        dyingSoundTimer += Time.deltaTime;
                    }
                    else if (dyingSoundTimer > 2.45f && !dyingSoundPlayed)
                    {
                        SoundManager.PlaySound("eyeDie2Sound");
                        dyingSoundPlayed = true;
                        door.SetActive(true);
                    }
                    anim.SetBool("hit", false);
                    anim.SetBool("dead", true);
                }
                else if (attackTimer >= 1.25f)
                {
                    state = prevState;
                }
                break;

            case movementState.funeralMarch:
                Debug.Log(Mathf.Abs(transform.position.x - startPos.x) < 0.1f);
                Debug.Log(Mathf.Abs(transform.position.y - startPos.y) < 0.1f);
                if (Mathf.Abs(transform.position.x - startPos.x) < 0.1f
                    && Mathf.Abs(transform.position.y - startPos.y) < 0.1f)
                {
                    Debug.Log("changed to stop");
                    prevState = movementState.funeralMarch;
                    state = movementState.stop;
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + (pathDir.x * 0.05f), transform.position.y + (pathDir.y * 0.05f));
                }
                break;
        }
      
        switch (currentPhase)
        {
            case Phase.firstPhase:
                if (cs.health <= 150)
                {
                    currentPhase = Phase.secondPhase;
                }
                break;

            case Phase.secondPhase:
                if (cs.health <= 75)
                {
                    currentPhase = Phase.finalPhase;
                }
                break;
        }

        if (cs.invincibilityLeft > 0)
        {
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        if (cs.health <= 0 && !(state == movementState.stop && prevState == movementState.funeralMarch))
        {
            pathDir = startPos - (Vector2)gameObject.transform.position;
            pathDir = pathDir.normalized;
            prevState = state;
            state = movementState.funeralMarch;
            dyingSoundTimer = 0;
            anim.SetBool("hit", true);
            pc.enabled = false;
        }
    }

}