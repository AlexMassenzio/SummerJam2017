using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour {

	private bool uvulaHit = false;
    private float hitSoundTimer, harpoonSoundTimer;

    private PlayerManager pm;

    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            pm = gameObject.GetComponent<PlayerManager>();
        }
        hitSoundTimer = 0;
        harpoonSoundTimer = 0;
    }

    private void Update()
    {
        hitSoundTimer -= Time.deltaTime;
        harpoonSoundTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // Object that is doing the hitting   
        if (gameObject.tag == "MackAttack" || gameObject.tag == "HarpoonAttack" || gameObject.tag == "BetterHarpoonAttack" || gameObject.tag == "BestHarpoonAttack")
        {
            WeaponStats ws = gameObject.GetComponent<WeaponStats>(); 

            // Check what type of object we collided with
            switch (col.tag)
            {
                // If MackAttack passed through an enemy
                case "Enemy":
                    if (hitSoundTimer <= 0)
                    {
                        SoundManager.PlaySound("hitSound");
                        hitSoundTimer = 1;
                    }
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    col.gameObject.SendMessage("Injure", ws.damage);
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
						EventManager.TriggerEvent("harpoonGetEvent");
					}
					break;

                case "EyeProjectile":
                    Destroy(col.gameObject);
                    break;
            }
        }
        else if (gameObject.tag == "Enemy" || gameObject.tag == "EyeProjectile")
        {
            // Check what type of object we collided with
            WeaponStats ws = gameObject.GetComponent<WeaponStats>();
            CharacterStats cs = col.gameObject.GetComponent<CharacterStats>();
            switch (col.tag)
            {
                case "Player":                    
                    if (cs.invincibilityLeft <= 0)
                    {
                        SoundManager.PlaySound("hitSound");
                        // Provide Mack with your damage info and tell him to injure himself
                        col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                        Debug.Log("knockback: " + ws.knockback);
                        col.gameObject.SendMessage("Knockback", ws.knockback);
                        col.gameObject.SendMessage("Injure", ws.damage);
                    }
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
                    break;

                case "WeaponPickup":
                    if (col.name == "AnchorPickup")
                    {
                        inv.WeaponGet("Anchor");
                    }
                    else if (col.name == "KnifePickup") {
                        inv.WeaponGet("Knife");
                    }
					else if (col.name == "HarpoonPickup")
                    {
                        pm.HarpoonGet();
                        EventManager.TriggerEvent("HarpoonGet");
					}
                    else if (col.name == "BetterHarpoonPickup")
                    {
                        pm.BetterHarpoonGet();
                        EventManager.TriggerEvent("BetterHarpoonGet");
                    }
                    else if (col.name == "BestHarpoonPickup")
                    {
                        pm.BestHarpoonGet();
                        EventManager.TriggerEvent("BestHarpoonGet");
                    }
                    Destroy(col.gameObject);
                    break;
            }
        }
        else if (gameObject.tag == "Weapon")
        {
            SoundManager.PlaySound("hitSound");
            WeaponStats ws = gameObject.GetComponent<WeaponStats>();
            switch (col.tag)
            {
                case "Enemy":
                    col.gameObject.SendMessage("Injure", ws.damage);
                    col.gameObject.SendMessage("Hitstun", ws.hitstunDuration);
                    Destroy(gameObject);
                    break;
            }

        }
    }

}