using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private GameObject player;
    private CapsuleCollider2D hb;
	private SpriteRenderer sr;
	private PlayerController pc;
    private MackAttack ma;
	private Animator ani;
    private CharacterStats cs;

    private float pillXPos;
    private float pillYPos;
    private float pillXSize;
    private float pillYSize;

    private bool dead;

	// Use this for initialization
	void Start () {
		try
		{
			player = GameObject.FindGameObjectsWithTag("Player")[0];
		}
		catch (Exception e)
		{
			Debug.LogError("Could not find any GameObject with the Tag \"player\"", this);
			Debug.LogException(e, this);
			Debug.Log("CameraFollow is turning off to prevent further issues.", this);
			enabled = false;
		}

        dead = false;

        hb = player.GetComponent<CapsuleCollider2D>();
		sr = player.GetComponent<SpriteRenderer>();
		pc = player.transform.parent.gameObject.GetComponent<PlayerController>();
        ma = player.transform.parent.gameObject.GetComponent<MackAttack>();
		ani = this.GetComponent<Animator>();
        cs = player.GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {

        // Vector used to modify Mack's hurt and hit boxes
        Vector2 newPos = hb.transform.position;
		bool moving = false;

        if (!ma.attacking)
        {
            // Moving to the left
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                // Flip sprite along X axis to the left
                sr.flipX = true;
                // Create new vector to update Mack's hurtbox with
                newPos = new Vector2(gameObject.transform.position.x-0.16f, gameObject.transform.position.y-0.1f);
                moving = true;
            }
            // Moving to the right
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                // Flip sprite along X axis to the right
                sr.flipX = false;
                // Create new vector to update Mack's hurtbox with
                newPos = new Vector2(gameObject.transform.position.x+0.16f, gameObject.transform.position.y-0.1f);
                moving = true;
            }
        }

        // Update Mack's hurtbox
        hb.transform.position = newPos;

        if (ma.attacking)
        {
            moving = false;
        }

        if (cs.health <= 0)
        {
            dead = true;
        }

        ani.SetBool("crouching", pc.crouching);
		ani.SetBool("grounded", pc.isGrounded());
		ani.SetBool("moving", moving);
        ani.SetBool("attacking", ma.attacking);
        ani.SetBool("dead", dead);
    }
}
