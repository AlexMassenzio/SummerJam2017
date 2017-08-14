using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * In Castlvania the axe goes 3/8 of the screen (6 Simon's in front of you), 
 * and is twice the height of Simon. It takes about a second to do a full arc.
 */

public class KnifeManager : PhysicsObject
{

    private Inventory inv;
    private WeaponStats ws;

    protected override void Start()
    {
        base.Start();

        inv = gameObject.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();

        ws.initVelocity = new Vector2();

        ws.damage = 2;
        ws.staminaCost = 2f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.3334f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2();

        velocity = ws.initVelocity;
        velocityX = ws.initVelocity.x;

        inv.WeaponUsed(ws.cooldownMax, ws.useStunDuration);
    }

    protected override void Update()
    {
        base.Update();
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    protected override void ComputeVelocity()
    {
        velocityX = 0.5f;
    }

}