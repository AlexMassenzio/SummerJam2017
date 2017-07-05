using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {
    private float timestamp;

    // TODO change this to something nice
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 1f;

    protected bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 targetVelocity;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
		timestamp = 0;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		//targetVelocity = Vector2.zero;
		ComputeVelocity();
	}

	protected virtual void ComputeVelocity()
	{

	}

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

        grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;

		Movement(move, 'x');

		move = Vector2.up * deltaPosition.y;

        Movement(move, 'y');

		timestamp += Time.deltaTime;
		if (gameObject.tag == "Player")
		{
			Debug.Log(gameObject.name);
			Debug.Log(velocity);
			Debug.Log(timestamp);
		}
	}

    void Movement(Vector2 move, char axis)
    {
        float distance = move.magnitude;

        // only check for collision if we are trying to move a certain distance
        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (axis == 'y')
                    {
						groundNormal = currentNormal;
						currentNormal.x = 0;
                    }
                }

				float projection = Vector2.Dot(velocity, currentNormal);
				if (projection < 0)
				{
					velocity -= projection * currentNormal;
				}

				float modifiedDistance = hitBufferList[i].distance - shellRadius;
				if (modifiedDistance < distance)
				{
					distance = modifiedDistance;
				}
			}

        }

        rb2d.position += move.normalized * distance;
    }

}
