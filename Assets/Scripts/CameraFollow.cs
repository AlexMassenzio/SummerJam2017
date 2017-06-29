using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private GameObject player;
	private Transform focus; //Which Transform the camera should track.

	public float zDistanceFromTarget = 10;
	public float zoomSpeed = 2;

	// Initialization
	void Start ()
	{
		try
		{
			player = GameObject.FindGameObjectsWithTag("Player")[0];
		}
		catch (Exception e)
		{
			Debug.LogError("Could not find any GameObject with the Tag \"player\"", this);
			Debug.LogException(e, this);
			Debug.Log("CameraFollow is turning off to prevent further issues.", this);
			enabled = false;
		}

		focus = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position + (focus.position - transform.position) * Time.deltaTime * 5;
		temp.z = focus.transform.position.z - zDistanceFromTarget;
		transform.position = temp;
	}
}
