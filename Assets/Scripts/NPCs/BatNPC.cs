using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNPC : PhysicsObject
{

    private WeaponStats ws;
    private Vector2 initPos;
    private float posY;

    protected override void Start()
    {
        base.Start();

        initPos = transform.position;

        ws = gameObject.GetComponent<WeaponStats>();
        ws.damage = 2;
        ws.staminaCost = 10f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.3334f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2();
    }

    protected override void FixedUpdate()
    {

        Vector2 move;

        transform.position = new Vector2(transform.position.x, posY + initPos.y);

        move = Vector2.right * velocityX;

        Movement(move, 'x');

        


        //Movement(move, 'y');

    }

    protected override void ComputeVelocity()
    {
        velocityX = -0.1f;
        posY = Mathf.Sin(Time.time * 3);
        Debug.Log("velY: " + velocityY);
    }

}