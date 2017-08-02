using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public DamageInfo myDamageInfo;
    public Vector2 initVelocity;
    public Vector2 knockback;
    public float staminaCost;
    public float cooldownMax;
    public float cooldownLeft;
    public float useStunDuration;
    public float hitstunDuration;

    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }
}