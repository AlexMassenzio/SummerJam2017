using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour {

	private bool uvulaHit = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Object that is doing the hitting   
        if (gameObject.tag == "MackAttack" || gameObject.tag == "HarpoonAttack")
        {
            WeaponStats ws = gameObject.transform.parent.GetComponentInChildren<WeaponStats>();
            // Check what type of object we collided with
            switch (col.tag)
            {
                // If MackAttack passed through an enemy
                case "Enemy":
                    col.gameObject.SendMessage("Injure", ws.damage);
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    break;

                case "Uvula":
					if (!uvulaHit)
					{
						GameObject harpoon = GameObject.Find("HarpoonPickup");
						float harpoonSmackX = harpoon.transform.position.x + 1;
						float harpoonSmackY = harpoon.transform.position.y;
						harpoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-850, 700));
						harpoon.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(0, 400), new Vector2(harpoonSmackX, harpoonSmackY));
						uvulaHit = true;
					}
					break;
            }
        }
        else if (gameObject.tag == "Enemy")
        {
            // Check what type of object we collided with
            WeaponStats ws = gameObject.GetComponent<WeaponStats>();
            switch (col.tag)
            {
                case "Player":
                    Debug.Log("Enemy hit Mack");
                    // Provide Mack with your damage info and tell him to injure himself
                    col.gameObject.SendMessage("Injure", ws.damage);
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    col.gameObject.SendMessage("Knockback", ws.knockback);
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
                    if (col.name == "AnchorPickup")
                    {
                        inv.WeaponGet("Anchor");
                    }
					else if (col.name == "HarpoonPickup")
					{
						EventManager.TriggerEvent("HarpoonGet");
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
                    col.gameObject.SendMessage("Injure", ws.damage);
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    col.gameObject.SendMessage("Knockback", ws.knockback);
                    break;
            }
        }
    }

}