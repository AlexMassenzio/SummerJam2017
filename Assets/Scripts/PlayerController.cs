using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	public float maxSpeed = 15f;
	public float jumpTakeOffSpeed = 30f;

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
		//Debug.Log("GetAxisRaw: " + move.x);
		
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

	public bool isGrounded()
	{
<<<<<<< HEAD
=======
		//Debug.Log("Grounded: " + grounded);
>>>>>>> master
		return grounded;
	}

}
