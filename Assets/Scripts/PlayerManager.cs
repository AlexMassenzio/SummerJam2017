using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private GameObject player;
	private SpriteRenderer sr;
	private PlayerController pc;
	private Animator ani;

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

		sr = player.GetComponent<SpriteRenderer>();
		pc = player.GetComponent<PlayerController>();
		ani = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		bool moving = false;

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

		ani.SetBool("grounded", pc.isGrounded());
		ani.SetBool("moving", moving);
	}
}
