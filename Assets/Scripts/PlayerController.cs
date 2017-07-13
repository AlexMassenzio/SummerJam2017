using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	public float maxSpeed = 12f;
    public float crouchSpeed = 5f;
	public float jumpTakeOffSpeed = 20f;
    public float speed;
    public bool crouching = false;

    // Use this for initialization
    /*
	void Start ()
    {

	}
    */

    private void OnEnable()
    {
        rb2d = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void ComputeVelocity()
	{

		Vector2 move = Vector2.zero;

		move.x = Input.GetAxisRaw("Horizontal");

        // Crouching
        if (Input.GetAxisRaw("Vertical") == -1 && grounded)
        {
            crouching = true;
            speed = crouchSpeed;
        }
        else
        {
            crouching = false;
            speed = maxSpeed;
        }

        // TODO double jumping?
        // Jumping
        if (Input.GetButtonDown("Jump") && grounded)
		{
			velocity.y = jumpTakeOffSpeed;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			if (velocity.y > 0)
			{
				velocity.y = velocity.y * 0.5f;
			}
		}

		targetVelocity = move * speed;

	}

	public bool isGrounded()
	{
		return grounded;
	}

}
