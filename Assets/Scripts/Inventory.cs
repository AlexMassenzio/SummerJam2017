using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool haveWeapon;
    public string weaponName;
    public float cooldownLeft = 0f;

    private void Start()
    {
        haveWeapon = false;
    }

    public void WeaponGet(string name)
    {
        haveWeapon = true;
        weaponName = name;
    }

    public void WeaponUsed(float cooldownDuration)
    {
        cooldownLeft = cooldownDuration;
    }

    public void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
        }
    }

}