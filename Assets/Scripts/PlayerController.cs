using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
    
	public float maxSpeed = 12f;
    public float crouchSpeed = 5f;
	public float jumpTakeOffSpeed = 20f;
    public float speed;
    public bool crouching = false;
	private float health = 100;

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
