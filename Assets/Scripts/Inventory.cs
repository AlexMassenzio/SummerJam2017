using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject mackInfo;
    public bool haveWeapon;
    public string weaponName;
    public float cooldownLeft = 0f;
    // Time that you're frozen after you use a weapon
    public float useStunLeft = 0f;

    private void Start()
    {
        haveWeapon = false;
        mackInfo = GameObject.FindGameObjectWithTag("MackInfo");
        if (mackInfo.GetComponent<MackWeaponInfo>().currentWeapon == MackWeaponInfo.Weapon.knife)
        {
            WeaponGet("Knife");
        }
        else if (mackInfo.GetComponent<MackWeaponInfo>().currentWeapon == MackWeaponInfo.Weapon.anchor)
        {
            WeaponGet("Anchor");
        }
    }

    public void WeaponGet(string name)
    {
        haveWeapon = true;
        weaponName = name;
        if (name == "Knife")
        {
            mackInfo.GetComponent<MackWeaponInfo>().currentWeapon = MackWeaponInfo.Weapon.knife;
        }
        else if (name == "Anchor")
        {
            mackInfo.GetComponent<MackWeaponInfo>().currentWeapon = MackWeaponInfo.Weapon.anchor;
        }
        else
        {
            mackInfo.GetComponent<MackWeaponInfo>().currentWeapon = MackWeaponInfo.Weapon.none;
        }
    }

    public void WeaponUsed(float cooldownDuration, float useStunDuration)
    {
        cooldownLeft = cooldownDuration;
        useStunLeft = useStunDuration;
    }

    public void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
        }

        if (useStunLeft > 0)
        {
            useStunLeft -= Time.deltaTime;
        }

    }

}