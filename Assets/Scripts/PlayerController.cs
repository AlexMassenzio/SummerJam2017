using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    private MackAttack ma;
    private CharacterStats cs;

    public int health = 100;
	public float maxSpeed = 10f;
    public float crouchSpeed;
	public float jumpTakeOffSpeed = 25f;
    public bool crouching = false;

    protected override void Start()
    {
        base.Start();

        crouchSpeed = maxSpeed / 3;

        ma = gameObject.GetComponent<MackAttack>();

        cs = gameObject.GetComponentInChildren<CharacterStats>();
        cs.myDamageInfo = new DamageInfo(5);
        cs.health = health;
        cs.maxSpeed = maxSpeed;
        cs.crouchSpeed = crouchSpeed;
        cs.jumpTakeOffSpeed = jumpTakeOffSpeed;
    }

    private void OnEnable()
    {
        rb2d = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
    }

	protected override void Update()
	{
		base.Update();
		if (health <= 0)
		{
			EventManager.TriggerEvent("MackDeath");
		}
	}

	protected override void ComputeVelocity()
	{
        Vector2 move = Vector2.zero;

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

		velocityX = move.x * cs.currentSpeed;
	}

	public bool isGrounded()
	{
		return grounded;
	}

}
