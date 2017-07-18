using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHostileCollision : MonoBehaviour {

	//private PlayerController pc;

	void Start()
	{
        //pc = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
    }
    /*
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag != "Player")
		{
			if (col.CompareTag("Enemy"))
			{
				Debug.Log("Detected hostile collision with Enemy");
				//col.SendMessageUpwards("GetDamageInfo", pc);
			}

		}
	} 
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Its a hit!");
        }
    }

}
