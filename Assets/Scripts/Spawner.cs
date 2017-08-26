using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject spawnee;

	public enum SpawnMode {Timed, OutOfRange};
	public SpawnMode mode;

	public float respawnTime;

	Coroutine waitAndRespawn;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (mode == SpawnMode.Timed)
		{
			if (waitAndRespawn == null)
			{
				waitAndRespawn = StartCoroutine(WaitAndSpawn());
			}
		}
		else if (mode == SpawnMode.OutOfRange)
		{

		}
	}

	IEnumerator WaitAndSpawn()
	{
		yield return new WaitForSeconds(respawnTime);

		Instantiate(spawnee, transform.position, transform.rotation);

		waitAndRespawn = null;
	}
}
