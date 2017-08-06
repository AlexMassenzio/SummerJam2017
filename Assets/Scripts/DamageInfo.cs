using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

	public int damageModifier;
    public float hitstunDuration = 0f;

    public DamageInfo(int damageModifier, float hitstun)
	{
		this.damageModifier = damageModifier;
        this.hitstunDuration = hitstun;
	}

    public void ChangeDamage(int newDamage)
    {
        this.damageModifier = newDamage;
    }

    public void ChangeHitstun()
    {

    }

}
