using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 5f;
	public float jumpForce = 500f;
	public float feetHeight = -2f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W))
		{
			rb.AddForce(Vector3.up * jumpForce);
		}
		Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + feetHeight, transform.position.z), new Vector3(transform.position.x, feetHeight-20, transform.position.z), Color.red);
		
	}

	private void FixedUpdate()
	{

		Vector3 newVelocity = new Vector3();
		newVelocity.y = rb.velocity.y;
		newVelocity.x = Input.GetAxis("Horizontal") * movementSpeed;
		rb.velocity = newVelocity;

		// Checks for collision with floor
		// Vector3 dwn = transform.TransformDirection(Vector3.down);
		Vector3 feet = new Vector3(transform.position.x, feetHeight, transform.position.z);

		if (Physics.Raycast(feet, Vector3.down, 10000))
		{
			Debug.Log("Colliding with floor");
		}
		else
		{
			Debug.Log("NOT Colliding with floor");
		}
			
	}
}
