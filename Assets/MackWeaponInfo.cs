using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MackWeaponInfo : MonoBehaviour {

    public enum AttackLevel { mackAttack, harpoon1, harpoon2, harpoon3 };
    public enum Weapon { none, knife, anchor };

    public AttackLevel currentAttack;
    public Weapon currentWeapon;

    private void Start()
    {
        DontDestroyOnLoad(this);
        currentAttack = AttackLevel.mackAttack;
        currentWeapon = Weapon.none;
    }

}
