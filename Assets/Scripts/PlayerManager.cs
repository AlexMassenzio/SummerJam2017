﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour {

	private GameObject player;

    private UnityAction harpoonListener;

    private CapsuleCollider2D upHurtBox;
    private BoxCollider2D crouchHurtBox;

    private SpriteRenderer sr;
	private PlayerController pc;
    private MackAttack ma;
	private Animator ani;
    private CharacterStats cs;
    private Inventory inv;
    private DetectTrigger dt;

    // Vectors used to modify Mack's hurt and hit boxes
    private Vector2 newPos;

    private bool dead;
    public bool throwing;
    private bool setNewPos;
    public bool changedDirection;
    public bool hasHarpoon;
    public bool hasBetterHarpoon;
    public bool hasBestHarpoon;
    public bool hit;

    private void Awake()
    {
        harpoonListener = new UnityAction(HarpoonGet);
    }

    private void OnEnable()
    {
        EventManager.StartListening("HarpoonGet", HarpoonGet);
    }

    // TODO: Figure out how to drop harpoon
    public void HarpoonGet()
    {
        hasHarpoon = true;
    }

    public void BetterHarpoonGet()
    {
        hasHarpoon = false;
        hasBetterHarpoon = true;
    }

    public void BestHarpoonGet()
    {
        hasHarpoon = false;
        hasBetterHarpoon = false;
        hasBestHarpoon = true;
    }

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
        throwing = false;
        setNewPos = false;
        changedDirection = false;
        hasHarpoon = false;
        hasBetterHarpoon = false;
        hasBestHarpoon = false;
        hit = false;

        upHurtBox = player.GetComponent<CapsuleCollider2D>();
        crouchHurtBox = player.GetComponent<BoxCollider2D>();

		sr = player.GetComponent<SpriteRenderer>();
		pc = player.transform.parent.gameObject.GetComponent<PlayerController>();
        ma = player.transform.parent.gameObject.GetComponent<MackAttack>();
		ani = this.GetComponent<Animator>();
        cs = player.GetComponent<CharacterStats>();
        inv = player.GetComponent<Inventory>();
        dt = player.GetComponent<DetectTrigger>();
        
        upHurtBox.enabled = true;
        crouchHurtBox.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

		bool moving = false;
        setNewPos = false;
        changedDirection = false;

        if (!ma.attacking && cs.hitstunLeft <= 0 && inv.useStunLeft <= 0)
        {
            // Moving to the left
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                // Check if we are swapping directions
                if (!sr.flipX)
                {
                    changedDirection = true;
                }
                // Flip sprite along X axis to the left
                sr.flipX = true;
                moving = true;
            }
            // Moving to the right
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                // Check if we are swapping directions
                if (sr.flipX)
                {
                    changedDirection = true;
                }
                // Flip sprite along X axis to the right
                sr.flipX = false;
                moving = true;
            }
        }

        if (pc.crouching)
        {
            upHurtBox.enabled = false;
            crouchHurtBox.enabled = true;
        }
        else
        {
            upHurtBox.enabled = true;
            crouchHurtBox.enabled = false;
        }

        if (cs.hitstunLeft > 0)
        {
            hit = true;
        }
        else
        {
            hit = false;
        }

        if (ma.attacking || cs.hitstunLeft > 0)
        {
            moving = false;
        }

        if (cs.health <= 0)
        {
            dead = true;
        }

        if (inv.useStunLeft > 0)
        {
            throwing = true;
        }
        else
        {
            throwing = false;
        }

        ani.SetBool("crouching", pc.crouching);
		ani.SetBool("grounded", pc.isGrounded());
		ani.SetBool("moving", moving);
        ani.SetBool("attacking", ma.attacking);
        ani.SetBool("hit", hit);
        ani.SetBool("dead", dead);
        ani.SetBool("throwing", throwing);
        ani.SetBool("harpoon", hasHarpoon);
        ani.SetBool("harpoon2", hasBetterHarpoon);
        ani.SetBool("harpoon3", hasBestHarpoon);
    }
}