using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public DamageInfo myDamageInfo;
    public int health;
    public float maxSpeed;
    public float crouchSpeed;
    public float jumpTakeOffSpeed;

    public void GetDamageInfo(CharacterStats cs)
    {
        cs.Injure(myDamageInfo);
    }

    public void Injure(DamageInfo di)
    {
        Debug.Log("INJURE");
        Debug.Log("Pre Health: " + health);
        health -= di.damageModifier;
        Debug.Log("GET UNNECESSARILY FUCKING BACKBOOSTED");
        Debug.Log("Post Health: " + health);
    }

}
