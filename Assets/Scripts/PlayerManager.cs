using System;
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
    private Vector2 newUpOffset;
    private Vector2 newUpSize;
    private Vector2 newCrouchOffset;
    private Vector2 newCrouchSize;
   // private Vector2 newMackPos;
    private Vector2 newPos;

    private bool dead;
    public bool throwing;
    private bool setNewPos;
    public bool changedDirection;
    public bool hasHarpoon;
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
    private void HarpoonGet()
    {
        hasHarpoon = true;
        cs.myDamageInfo.ChangeDamage(10);
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

        // Default to facing right upright
        newUpOffset = new Vector2(0.16f, -0.1f);
        newUpSize = new Vector2(1.4f, 3.3f);
        newCrouchOffset = new Vector2(0.16f, -0.28f);
        newCrouchSize = new Vector2(1.61f, 2.73f);
        upHurtBox.enabled = true;
        crouchHurtBox.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

		bool moving = false;
        setNewPos = false;
        changedDirection = false;

        if (!ma.attacking)
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

        // Mack is looking left
        if (sr.flipX)
        {
            if (pc.crouching)
            {
                newCrouchOffset = new Vector2(-0.16f, -0.28f);
                newCrouchSize = new Vector2(1.58f, 2.5f);
                // If we are coming from a standing position, teleport down a little
                if (upHurtBox.enabled)
                {
                    newPos = new Vector2(player.transform.position.x, player.transform.position.y - 0.21f);
                    setNewPos = true;
                }
                upHurtBox.enabled = false;
                crouchHurtBox.enabled = true;
            }
            else
            {
                newUpOffset = new Vector2(-0.16f, -0.1f);
                newUpSize = new Vector2(1.4f, 3.3f);
                // If we are going from crouching to standing, teleport up a little
                if (crouchHurtBox.enabled)
                {
                    newPos = new Vector2(player.transform.position.x, player.transform.position.y + 0.21f);
                    setNewPos = true;
                }
                upHurtBox.enabled = true;
                crouchHurtBox.enabled = false;
            }
        }
        // Mack is looking right
        else
        {
            if (pc.crouching)
            {
                newCrouchOffset = new Vector2(0.16f, -0.28f);
                newCrouchSize = new Vector2(1.58f, 2.5f);
                // If we are coming from a standing position, teleport down a little
                if (upHurtBox.enabled)
                {
                    newPos = new Vector2(player.transform.position.x, player.transform.position.y - 0.21f);
                    setNewPos = true;
                }
                upHurtBox.enabled = false;
                crouchHurtBox.enabled = true;
            }
            else
            {
                newUpOffset = new Vector2(0.16f, -0.1f);
                newUpSize = new Vector2(1.4f, 3.3f);
                // If we are going from crouching to standing, teleport up a little
                if (crouchHurtBox.enabled)
                {
                    newPos = new Vector2(player.transform.position.x, player.transform.position.y+0.21f);
                    setNewPos = true;
                }
                upHurtBox.enabled = true;
                crouchHurtBox.enabled = false;
            }
        }

        // Update Mack's position if he needed to be shifted
        if (setNewPos)
        {
            player.transform.position = newPos;
        }

        // Update Mack's hurt and hitboxes
        upHurtBox.offset = newUpOffset;
        upHurtBox.size = newUpSize;
        crouchHurtBox.offset = newCrouchOffset;
        crouchHurtBox.size = newCrouchSize;

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
    }
}
