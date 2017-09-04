using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNPC : PhysicsObject
{
    public GameObject leftBound;
    public GameObject rightBound;
    public enum Direction { left, right }
    public Direction startingDir;
    private GameObject character;
    private BoxCollider2D bodyHitbox;
    private WeaponStats ws;
    private CharacterStats NPCcs;
    private Vector2 initPos;
    private Animator ani;
    private float posY;
    public float oscillationFactor;
    public float oscillationTimer;

    protected override void Start()
    {
        base.Start();
        leftBound = GameObject.FindGameObjectWithTag("LeftRoomBounds");
        rightBound = GameObject.FindGameObjectWithTag("RightRoomBounds");
        character = GameObject.FindGameObjectWithTag("Player");
        ani = gameObject.GetComponent<Animator>();
        bodyHitbox = gameObject.GetComponent<BoxCollider2D>();
        initPos = transform.position;
        oscillationTimer = Time.time;
        oscillationFactor = 4;

        NPCcs = gameObject.GetComponent<CharacterStats>();
        ws = gameObject.GetComponent<WeaponStats>();
        ws.damage = 2;
        ws.hitstunDuration = 0.3334f;
    }

    protected override void Update()
    {
        base.Update();

        if (NPCcs.invincibilityLeft > 0)
        {
            ani.SetBool("hit", true);
        }
        else
        {
            ani.SetBool("hit", false);
        }

        if (NPCcs.health <= 0 || transform.position.x < leftBound.transform.position.x || transform.position.x > rightBound.transform.position.x)
        {
            NPCcs.health = 0;
            bodyHitbox.enabled = false;
            ani.SetBool("dead", true);
        }
    }

    protected override void FixedUpdate()
    {

        Vector2 move;

        transform.position = new Vector2(transform.position.x, posY + initPos.y);

        move = Vector2.right * velocityX;

        Movement(move, 'x');

    }

    protected override void ComputeVelocity()
    {
        if (NPCcs.hitstunLeft > 0 || NPCcs.dying || NPCcs.dead)
        {
            velocityX = 0;
            velocityY = 0;
            velocity = new Vector2();
        }
        else
        {
            velocityX = -cs.maxSpeed;
            posY = Mathf.Sin(oscillationTimer * oscillationFactor);
            oscillationTimer += Time.deltaTime;
        }
        
    }

}