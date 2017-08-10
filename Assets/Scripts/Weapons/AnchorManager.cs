using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * In Castlvania the axe goes 3/8 of the screen (6 Simon's in front of you), 
 * and is twice the height of Simon. It takes about a second to do a full arc.
 */

public class AnchorManager : PhysicsObject {
    
    private Inventory inv;
    private WeaponStats ws;
    //private SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();
        
        inv = gameObject.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();
        //sr = gameObject.GetComponent<SpriteRenderer>();

        // If Mack is facing to the right
        if (!sr.flipX)
        {
            ws.initVelocity = new Vector2(12.5f, 25);
        }
        else
        {
            ws.initVelocity = new Vector2(-12.5f, 25);
        }

        // TODO: Tweak these values to what feels best in game
        ws.damage = 5;
        ws.staminaCost = 5f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.5f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2(5f, 5f);

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
   
}