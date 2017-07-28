using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

    public DamageInfo myDamageInfo;
    public Vector2 initVelocity;
    public Vector2 knockback;
    public float weight;
    public float staminaCost;
    public float cooldownMax;
    public float cooldownLeft;
    public float hitstunDuration;

    /*
    public CharacterStats(DamageInfo myDI, Velocity2 initVel, Vector2 knockback, float weight, 
                          float stamina, float cooldown, float hitstun)
    {
        this.myDamageInfo = myDI;
        this.initVelocity = initVel;
        this.knockback = knockback;
        this.weight = weight;
        this.staminaCost = stamina;
        this.cooldownMax = cooldown;
        this.hitstunDuration = hitstun;
    }
    */

    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }
}