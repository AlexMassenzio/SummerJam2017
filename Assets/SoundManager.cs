using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip mackAttackSound, hitSound, dieSound, harpoonSound, bestHarpoonSound, eyeDie1Sound, eyeDie2Sound;
    static AudioSource audioSrc;

	void Start () {

        mackAttackSound = Resources.Load<AudioClip>("mack_attack_sound");
        hitSound = Resources.Load<AudioClip>("hit_sound");
        dieSound = Resources.Load<AudioClip>("die_sound");
        harpoonSound = Resources.Load<AudioClip>("harpoon_sound");
        bestHarpoonSound = Resources.Load<AudioClip>("harpoon_lvl3_sound");
        eyeDie2Sound = Resources.Load<AudioClip>("eye_rest_sound");

        audioSrc = gameObject.GetComponent<AudioSource>();

	}

    public static void PlaySound (string clip)
    {
        audioSrc.volume = 0.5f;
        switch (clip)
        {
            case "mackAttackSound":
                audioSrc.PlayOneShot(mackAttackSound);
                break;

            case "hitSound":
                audioSrc.PlayOneShot(hitSound);
                break;

            case "dieSound":
                //audioSrc.PlayOneShot(dieSound);
                break;
               
            case "harpoonSound":
                audioSrc.PlayOneShot(harpoonSound);
                break;

            case "bestHarpoonSound":
                audioSrc.PlayOneShot(bestHarpoonSound);
                break;

            case "eyeDie1Sound":
                audioSrc.PlayOneShot(eyeDie1Sound);
                break;

            case "eyeDie2Sound":
                audioSrc.PlayOneShot(eyeDie2Sound);
                break;
        }
    }

}
