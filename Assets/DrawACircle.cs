using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawACircle : MonoBehaviour {

    public float timeCounter;
    public float x, y, radius, speed;

	// Use this for initialization
	void Start () {
        timeCounter = Time.time;
        speed = 5;
        radius = 5f;
    }
	
	// Update is called once per frame
	/*void Update () {

        timeCounter += Time.deltaTime * speed;

        x = (Mathf.Cos(timeCounter) * radius) + transform.position.x;
        y = (Mathf.Sin(timeCounter) * radius) + transform.position.y;
        
        Debug.DrawLine(transform.position, new Vector2(x, y));

    }*/
}
