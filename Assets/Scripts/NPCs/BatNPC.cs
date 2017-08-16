using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNPC : PhysicsObject
{

    private WeaponStats ws;
    private Vector2 initPos;
    private float posY;
    public float oscillationFactor;

    protected override void Start()
    {
        base.Start();

        sr.flipX = true;

        initPos = transform.position;
        oscillationFactor = 4;

        ws = gameObject.GetComponent<WeaponStats>();
        ws.damage = 2;
        ws.hitstunDuration = 0.3334f;
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
        velocityX = -0.1f;
        posY = Mathf.Sin(Time.time * oscillationFactor);
    }

}