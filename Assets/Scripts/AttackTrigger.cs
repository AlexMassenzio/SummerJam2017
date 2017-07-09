using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    // TODO set this to something meaningful
    public int dmg = 20;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Debug.Log("In OnTriggerEnter2D with NOT Mack");
            if (col.isTrigger == false && col.CompareTag("Enemy"))
            {
                Debug.Log("Detected collision with enemy");
                col.SendMessageUpwards("Damage", dmg);
            }

        }

    }

}
