using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectileManager : PhysicsObject
{

    private GameObject eye;
    private Vector2 mackPos;
    public Vector2 throwDir;

    protected override void Start()
    {
        base.Start();

        eye = GameObject.FindGameObjectWithTag("Enemy");

        // Get Vector pointing from eye to mack
        mackPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log("mackPos: " + mackPos);
        throwDir = mackPos - (Vector2)eye.transform.position;
        Debug.Log("throwDir: " + throwDir);
        throwDir = throwDir.normalized;
        Debug.Log("Normalized throwDir: " + throwDir);
        velocity = throwDir;
        Debug.Log("velocity: " + velocity);
    }

    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(eye.transform.position, gameObject.transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    protected override void ComputeVelocity()
    {
        velocity = throwDir;
        rb2d.position += velocity * 0.1f;
    }

}