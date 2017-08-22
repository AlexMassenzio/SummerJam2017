using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNPC : PhysicsObject
{

    private WeaponStats ws;
    private CharacterStats cs;
    private Animator ani;
    private Vector2 initPos;
    private float posY;
    public float oscillationFactor;

    protected override void Start()
    {
        base.Start();

        initPos = transform.position;
        oscillationFactor = 4;

        cs = gameObject.GetComponent<CharacterStats>();
        ani = gameObject.GetComponent<Animator>();
        ws = gameObject.GetComponent<WeaponStats>();
        ws.damage = 2;
        ws.hitstunDuration = 0.3334f;
    }

    protected override void Update()
    {
        base.Update();

        if (cs.hitstunLeft > 0)
        {
            ani.SetBool("hit", true);
        }
        else
        {
            ani.SetBool("hit", false);
        }

        if (cs.health <= 0)
        {
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
        if (cs.hitstunLeft > 0)
        {
            velocityX = 0;
            velocityY = 0;
            velocity = new Vector2();
        }
        else
        {
            velocityX = -0.2f;
        }
        posY = Mathf.Sin(Time.time * oscillationFactor);
    }

}