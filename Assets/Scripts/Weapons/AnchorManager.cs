using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * In Castlvania the axe goes 3/8 of the screen (6 Simon's in front of you), 
 * and is twice the height of Simon. It takes about a second to do a full arc.
 */

public class AnchorManager : PhysicsObject {

    private GameObject mack;
    private Inventory inv;
    private WeaponStats ws;

    protected override void Start()
    {
        base.Start();

        mack = GameObject.FindWithTag("Player");
        inv = mack.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();

        // If Mack is facing to the right
        if (!mack.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            ws.initVelocity = new Vector2(12.5f, 25);
        }
        else
        {
            ws.initVelocity = new Vector2(-12.5f, 25);
        }

        // TODO: Tweak these values to what feels best in game
        ws.myDamageInfo = new DamageInfo(5, 2f);
        ws.weight = 5f;
        ws.staminaCost = 5f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.5f;
        ws.knockback = new Vector2(-5f, 5f);

        velocity = ws.initVelocity;
        velocityX = ws.initVelocity.x;

        inv.WeaponUsed(ws.cooldownMax);
    }
    
    protected override void Update()
    {
        base.Update();
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }    
    }
   
}