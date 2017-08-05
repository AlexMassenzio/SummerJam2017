using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private GameObject player;
	private Vector3 focus; //Which Transform the camera should track.

	public float zDistanceFromTarget = 10;
	public float zoomSpeed = 2;

	private float screenWidth;

	//Camera bounds properties
	private const float CAMERA_THRESHOLD = 0.3f;
	private const float CAMERA_SNAP = 0.15f;
	enum SnapBound {Left, Right};
	private SnapBound currentSnap;

	private Coroutine boundTransitioning;

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

		focus = player.transform.position;

		screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;

		currentSnap = SnapBound.Left;
	}

	// Update is called once per frame
	void Update()
	{

		float leftSnap = transform.position.x - screenWidth * CAMERA_SNAP;
		float leftThreshold = transform.position.x - screenWidth * CAMERA_THRESHOLD;
		Debug.DrawLine(new Vector3(leftSnap, transform.position.y, 0), new Vector3(leftSnap, transform.position.y + 5, 0), Color.blue);
		Debug.DrawLine(new Vector3(leftThreshold, transform.position.y, 0), new Vector3(leftThreshold, transform.position.y + 5, 0), Color.blue);

		float rightSnap = transform.position.x + screenWidth * CAMERA_SNAP;
		float rightThreshold = transform.position.x + screenWidth * CAMERA_THRESHOLD;
		Debug.DrawLine(new Vector3(rightSnap, transform.position.y, 0), new Vector3(rightSnap, transform.position.y + 5, 0), Color.blue);
		Debug.DrawLine(new Vector3(rightThreshold, transform.position.y, 0), new Vector3(rightThreshold, transform.position.y + 5, 0), Color.blue);

		if (boundTransitioning == null)
		{
			if (currentSnap == SnapBound.Left)
			{
				Debug.Log("Left");

				if (player.transform.position.x > leftSnap)
				{
					Debug.Log("Over snap bound");
					focus.x = player.transform.position.x + (transform.position.x - leftSnap);
				}
				else if (player.transform.position.x < leftThreshold)
				{
					Debug.Log("changing direction");
					currentSnap = SnapBound.Right;
					boundTransitioning = StartCoroutine(BoundTransition());
				}
			}
			else
			{
				Debug.Log("Right");

				if (player.transform.position.x < rightSnap)
				{
					Debug.Log("Over snap bound");
					focus.x = player.transform.position.x - (rightSnap - transform.position.x);
				}
				else if (player.transform.position.x > rightThreshold)
				{
					Debug.Log("changing direction");
					currentSnap = SnapBound.Left;
					boundTransitioning = StartCoroutine(BoundTransition());
				}
			}
		}

		focus.z = -20;
		transform.position = focus;
	}

	IEnumerator BoundTransition()
	{
		float start = transform.position.x;
		float progress = 0;
		float target;

		if (currentSnap == SnapBound.Left)
		{
			while (progress < 1)
			{
				target = player.transform.position.x + (transform.position.x - (transform.position.x - screenWidth * CAMERA_SNAP));
				focus.x = Mathf.Lerp(start, target, progress);
				progress += Time.deltaTime;
				yield return null;
			}
		}
		else
		{
			while (progress < 1)
			{
				target = player.transform.position.x - ((transform.position.x + screenWidth * CAMERA_SNAP) - transform.position.x);
				focus.x = Mathf.Lerp(start, target, progress);
				progress += Time.deltaTime;
				yield return null;
			}
		}

		
		boundTransitioning = null;
	}
}
