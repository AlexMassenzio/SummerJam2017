using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : PhysicsObject {

    private GameObject mack;
    private Inventory inv;
    private WeaponStats ws;

    protected override void Start()
    {
        base.Start();

        mack = GameObject.FindWithTag("Player");
        inv = mack.GetComponentInChildren<Inventory>();

        Vector2 initVelocity;
        // If Mack is facing to the right
        if (!mack.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 1000));
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 1000));
        }

        ws = gameObject.GetComponent<WeaponStats>();
        
        // TODO: Tweak these values to what feels best in game
        ws.myDamageInfo = new DamageInfo(5);
        ws.weight = 5f;
        ws.staminaCost = 5f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.5f;
        ws.knockback = new Vector2(-5f, 5f);

        Movement(ws.initVelocity, 'x');
        Movement(ws.initVelocity, 'y');

        inv.WeaponUsed(ws.cooldownMax);
    }
    
    private void Update()
    {
        base.Update();
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }    
    }
   
}