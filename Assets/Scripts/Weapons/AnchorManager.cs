using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * In Castlvania the axe goes 3/8 of the screen (6 Simon's in front of you), 
 * and is twice the height of Simon. It takes about a second to do a full arc.
 */

public class AnchorManager : PhysicsObject {

    public GameObject mack;
    private Inventory inv;
    private WeaponStats ws;
    private SpriteRenderer sre;

    protected override void Start()
    {
        base.Start();

        mack = GameObject.FindGameObjectWithTag("Player");
        inv = mack.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();
        sre = mack.GetComponent<SpriteRenderer>();

        // If Mack is facing to the right
        if (!sre.flipX)
        {
            ws.initVelocity = new Vector2(0.225f, 27);
        }
        // If Mack is facing to the left
        else
        {
            ws.initVelocity = new Vector2(-0.225f, 27);
        }

        ws.damage = 5;
        ws.staminaCost = 5f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.5f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2();

        velocity = ws.initVelocity;

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
        velocityX = ws.initVelocity.x;
        base.ComputeVelocity();
    }

}