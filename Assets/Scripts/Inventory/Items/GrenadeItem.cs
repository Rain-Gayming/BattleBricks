using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GrenadeItem : ItemObject
{
    [BoxGroup("Grenade Info")]
    public float damage;
    [BoxGroup("Grenade Info")]
    public float velocity;
    [BoxGroup("Grenade Info")]
    public float fragments;
}
