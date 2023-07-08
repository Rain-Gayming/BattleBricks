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
}

public enum Caliber
{

}
