using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Object that is doing the hitting   
        if (gameObject.tag == "MackAttack")
        {
            // Check what type of object we collided with
            switch (col.tag)
            {
                // If MackAttack passed through an enemy
                case "Enemy":
                    // Provide the enemy with Mack's damage info and tell him to injure himself
                    col.gameObject.SendMessage("Injure", gameObject.transform.parent.GetComponentInChildren<CharacterStats>().myDamageInfo);
                    break;

                case "Uvula":
                    Debug.Log("HALP HITLER ME");
                    // TODO: Trigger harpoon being thrown up
                    GameObject harpoon = GameObject.Find("Harpoon");
                    harpoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-800f, 600f));
                    break;

            }
        }
        else if (gameObject.tag == "Enemy")
        {
            // Check what type of object we collided with
            switch (col.tag)
            {
                case "Player":
                    // Provide Mack with your damage info and tell him to injure himself
                    col.gameObject.SendMessage("Injure", gameObject.GetComponent<CharacterStats>().myDamageInfo);
                    break;
            }
        }
        else if (gameObject.tag == "Player")
        {
            switch (col.tag)
            {
                case "Door":
                    // TODO: Room change logic
                    Debug.Log("Bye bye");
                    break;

                case "Weapon":
                    // TODO: Inventory system
                    Debug.Log("Touched a weapon");
                    Destroy(col.gameObject);
                    break;
            }
        }
    }

}
