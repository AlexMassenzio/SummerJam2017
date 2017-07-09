using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MackAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0;
    // TODO set this to something correct
    private float attackCooldown = 0.3f;

    public Collider2D attackTrigger;

    // TODO @Alex make sure this works with our animation system
    //private Animator anim;

    private void Awake()
    {
        // TODO @Alex make sure this works with our animation system
        //anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    private void Update()
    {
        // TODO agree on attack key
        // GetMouseButtonDown(0) is left click, 1 is right, and 2 is middle
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;

            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;

            }
        }
    }

}
