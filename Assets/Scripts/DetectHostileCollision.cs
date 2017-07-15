using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHostileCollision : MonoBehaviour {

	private PlayerController pc;

	void Start()
	{
		pc = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag != "Player")
		{
			if (col.isTrigger == false && col.CompareTag("Enemy"))
			{
				Debug.Log("Detected hostile collision with Enemy");
				col.SendMessageUpwards("GetDamageInfo", pc);
			}

		}
	} 

}
