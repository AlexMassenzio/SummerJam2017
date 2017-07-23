using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private GameObject player;
	private SpriteRenderer sr;
	private PlayerController pc;
    private MackAttack ma;
	private Animator ani;
    private CharacterStats cs;

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

		sr = player.GetComponent<SpriteRenderer>();
		pc = player.transform.parent.gameObject.GetComponent<PlayerController>();
        ma = player.transform.parent.gameObject.GetComponent<MackAttack>();
		ani = this.GetComponent<Animator>();
        cs = player.GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {

		bool moving = false;

        if (!ma.attacking)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                sr.flipX = true;
                moving = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                sr.flipX = false;
                moving = true;
            }
        }

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
