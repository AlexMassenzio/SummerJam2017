using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	public float maxSpeed = 7f;
	public float jumpTakeOffSpeed = 7f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis("Horizontal");

		// TODO double jumping
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

		targetVelocity = move * maxSpeed;
	}

}
