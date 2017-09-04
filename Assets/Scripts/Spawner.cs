using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject spawnee;

	public enum SpawnMode {Timed, OutOfRange};
    public enum Direction { left, right };
    public Direction pointEnemies;
	public SpawnMode mode;

	public float respawnTime; //Set respawnTime to 0 for 1 spawn per time the player enters view

	Coroutine waitAndRespawn;

	// Use this for initialization
	void Start () {
		waitAndRespawn = null;
		Debug.Log(GetComponent<Renderer>().name);
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
			if(GetComponent<Renderer>().isVisible)
			{
				if (waitAndRespawn == null)
				{
					waitAndRespawn = StartCoroutine(WaitAndSpawn());
				}
			}
			else if(waitAndRespawn != null)
			{
				StopCoroutine(waitAndRespawn);
				waitAndRespawn = null;
			}
		}
	}

	IEnumerator WaitAndSpawn()
	{
		if (respawnTime == 0)
		{
			GameObject newEnemy = Instantiate(spawnee, transform.position, transform.rotation);
            newEnemy.GetComponent<CharacterStats>().startingDir = (CharacterStats.Direction)pointEnemies;
            Debug.Log("Set enemy's starting dir to " + newEnemy.GetComponent<CharacterStats>().startingDir);
            while (true) { yield return null; } //Wait to be killed
		}
		else
		{
			while (true)
			{
				GameObject newEnemy = Instantiate(spawnee, transform.position, transform.rotation);
                newEnemy.GetComponent<CharacterStats>().startingDir = (CharacterStats.Direction)pointEnemies;
                Debug.Log("Set enemy's starting dir to " + newEnemy.GetComponent<CharacterStats>().startingDir);
				yield return new WaitForSeconds(respawnTime);
			}
		}

	}
}
