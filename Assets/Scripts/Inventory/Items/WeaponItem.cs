using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WeaponItem : ItemObject
{
    [BoxGroup("Weapon Info")]
    public float damage;
    [BoxGroup("Weapon Info")]
    public float attackTime;
}
