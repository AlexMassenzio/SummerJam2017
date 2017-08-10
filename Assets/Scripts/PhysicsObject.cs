using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {
	
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 5f;

    protected bool grounded;
    protected Vector2 groundNormal;
	public float velocityX;
	public float velocityY
	{
		get { return velocity.y; }
		set { velocity.y = value; }
	}
    protected Rigidbody2D rb2d;
    public Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected CharacterStats cs;
    protected SpriteRenderer sr;

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Start ()
    {
        // Ignore any contacts involving trigger colliders
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

        if (gameObject.tag == "Player")
        {
            cs = gameObject.GetComponentInChildren<CharacterStats>();
        }
        else if (gameObject.tag == "Enemy")
        {
            cs = gameObject.GetComponent<CharacterStats>();
        }
	}
	
	protected virtual void Update ()
    {
        ComputeVelocity();
    }

	protected virtual void ComputeVelocity()
	{

	}

    protected virtual void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        if (cs.hitstunLeft > 0)
        {
            // If facing left
            if (sr.flipX)
            {
                velocity = new Vector2(15, velocity.y);
            }
            // If we are facing right
            else
            {
                velocity = new Vector2(-15, velocity.y);
            }
        }
        else
        {
            velocity.x = velocityX;
        }
        

        grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move;

        // If we are actionable Mack
        if (gameObject.tag == "Weapon" || cs.hitstunLeft <= 0)
        {
            move = moveAlongGround * deltaPosition.x;
        }
        else
        {
            move = Vector2.right * deltaPosition.x;
        }

        Movement(move, 'x');
        
        move = Vector2.up * deltaPosition.y;

        Movement(move, 'y');

    }

    protected void Movement(Vector2 move, char axis)
    {
        // Distance that object is going to move
        float distance = move.magnitude;

        // Only check for collision if we are trying to move 
        if (distance > minMoveDistance && gameObject.tag != "Weapon")
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                // Vector perpendicular to the RayCast which detected collision and with what it collided
                Vector2 currentNormal = hitBufferList[i].normal;

                // If what we collided with is flat enough to be considered ground
                //Debug.Log("currentNormalY: " + currentNormal.y);
                if (currentNormal.y > minGroundNormalY)
                {
                    // Set grounded equal to true
                    grounded = true;
                    // If we are modifying the object's y coordinate
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