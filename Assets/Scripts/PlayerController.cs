using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    private MackAttack ma;
    private Inventory inv;

    public GameObject anchor;

    // TODO: put CharacterStats fields here

    public bool crouching = false;

    protected override void Start()
    {
        base.Start();

        ma = gameObject.GetComponent<MackAttack>();
        inv = gameObject.GetComponentInChildren<Inventory>();

        cs = gameObject.GetComponentInChildren<CharacterStats>();
        cs.myDamageInfo = new DamageInfo(5, 2f);
        cs.health = 100;
        cs.maxSpeed = 10f;
        cs.crouchSpeed = cs.maxSpeed / 3;
        cs.jumpTakeOffSpeed = 25f;
        cs.stamina = 100;

    }

    private void OnEnable()
    {
        rb2d = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Update() {

        base.Update();

        // Use Weapon
        if (Input.GetMouseButtonDown(1) && inv.haveWeapon && cs.hitstunLeft <= 0)
        {
            if (inv.cooldownLeft <= 0)
            {
                if (inv.weaponName == "Anchor")
                {
                    Instantiate(anchor, transform.GetChild(0).position, new Quaternion());
                }
            }
        }

        if (cs.hitstunLeft > 0)
        {
            Debug.Log("In hitstun");
        }
    }

    protected override void ComputeVelocity()
	{
        Vector2 move = Vector2.zero;

        if (cs.hitstunLeft <= 0)
        {
            if (!grounded || !ma.attacking)
            {
                move.x = Input.GetAxisRaw("Horizontal");
            }

            // Crouching
            if (Input.GetAxisRaw("Vertical") == -1 && (grounded || crouching))
            {
                crouching = true;
                cs.currentSpeed = cs.crouchSpeed;
            }
            else
            {
                crouching = false;
                cs.currentSpeed = cs.maxSpeed;
            }

            // Jumping
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocityY = cs.jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocityY > 0)
                {
                    velocityY = velocity.y * 0.5f;
                }
            }
        }

		velocityX = move.x * cs.currentSpeed;
	}

	public bool isGrounded()
	{
		return grounded;
	}

}