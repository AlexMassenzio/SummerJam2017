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
                    GameObject harpoon = GameObject.Find("Harpoon");
                    harpoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-800f, 600f));
                    break;

            }
        }
        else if (gameObject.tag == "Enemy")
        {
            // Check what type of object we collided with
            CharacterStats cs = gameObject.GetComponent<CharacterStats>();
            switch (col.tag)
            {
                case "Player":
                    // Provide Mack with your damage info and tell him to injure himself
                    col.gameObject.SendMessage("Injure", cs.myDamageInfo);
                    col.gameObject.SendMessage("Hitstun", cs.myDamageInfo);
                    //col.gameObject.SendMessage("Knockback", cs.knockback);
                    break;
            }
        }
        // Mack's hurtbox
        else if (gameObject.tag == "Player")
        {
            Inventory inv = gameObject.GetComponent<Inventory>();
            switch (col.tag)
            {
                case "Door":
                    // TODO: Room change logic
                    Debug.Log("Bye bye");
                    break;

                case "WeaponPickup":
                    Debug.Log(col.name + " get!");
                    if (col.name == "AnchorPickup")
                    {
                        inv.WeaponGet("Anchor");
                    }
                    Destroy(col.gameObject);
                    break;
            }
        }
        else if (gameObject.tag == "Weapon") 
        {
            WeaponStats ws = gameObject.GetComponent<WeaponStats>();
            switch (col.tag)
            {
                case "Enemy":
                    col.gameObject.SendMessage("Injure", ws.myDamageInfo);
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    col.gameObject.SendMessage("Knockback", ws.knockback);
                    break;
            }
        }
    }

}