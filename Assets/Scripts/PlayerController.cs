using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	private float health = 100;
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
			velocityY = jumpTakeOffSpeed;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			if (velocityY > 0)
			{
				velocityY = velocity.y * 0.5f;
			}
		}

		velocityX = move.x * maxSpeed;
	}

	public bool isGrounded()
	{
		//Debug.Log("Grounded: " + grounded);
		return grounded;
	}

	public void Injure(DamageInfo di)
	{
		Debug.Log("INJURE");
		Debug.Log("Mack Health: " + health);
		health -= di.damageModifier;
		Debug.Log("GET UNNECESSARILY FUCKING BACKBOOSTED");
		Debug.Log("Mack Health: " + health);
	}

}
