using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BulletItem : ItemObject
{
    [BoxGroup("Bullet Info")]
    public float velocity;
    [BoxGroup("Bullet Info")]
    public float damage;
    [BoxGroup("Bullet Info")]
    public Caliber caliber;
    [BoxGroup("Bullet Info")]
    public string bulletName;
}

public enum Caliber
{
    _45acp,
    _556x45Nato
}
