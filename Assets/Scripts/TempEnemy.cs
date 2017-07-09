using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour {

    int curHealth = 100;

	public void Damage (int damage)
    {
        Debug.Log("got hit");
        curHealth -= damage;
    }

}
