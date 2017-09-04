using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip mackAttackSound, mackJumpSound, mackWalkSound, hitSound, harpoonSound, slimeDeathSound, batDeathSound, zombieDeathSound;
    static AudioSource audioSrc;

	void Start () {

        mackAttackSound = Resources.Load<AudioClip>("mack_attack_sound");
        mackJumpSound = Resources.Load<AudioClip>("mack_jump_sound");
        mackWalkSound = Resources.Load<AudioClip>("mack_walk_sound");
        hitSound = Resources.Load<AudioClip>("hit_sound");
        harpoonSound = Resources.Load<AudioClip>("harpoon_sound");
        slimeDeathSound = Resources.Load<AudioClip>("slime_death_sound");
        batDeathSound = Resources.Load<AudioClip>("bat_death_sound");
        zombieDeathSound = Resources.Load<AudioClip>("zombie_death_sound");

        audioSrc = gameObject.GetComponent<AudioSource>();

	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "mackAttackSound":
                audioSrc.PlayOneShot(mackAttackSound);
                break;

            case "mackJumpSound":
                audioSrc.PlayOneShot(mackJumpSound);
                break;

            case "mackWalkSound":
                audioSrc.PlayOneShot(mackWalkSound);
                break;

            case "hitSound":
                audioSrc.PlayOneShot(hitSound);
                break;

            case "harpoonSound":
                audioSrc.PlayOneShot(harpoonSound);
                break;

            case "slimeDeathSound":
                audioSrc.PlayOneShot(slimeDeathSound);
                break;

            case "batDeathSound":
                audioSrc.PlayOneShot(batDeathSound);
                break;

            case "zombieDeathSound":
                audioSrc.PlayOneShot(zombieDeathSound);
                break;
        }
    }

}
