using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GunItem : ItemObject
{
    [BoxGroup("Gun Info")]
    public int animLayer;
    [BoxGroup("Gun Info")]
    public float fireRateAuto;
    [BoxGroup("Gun Info")]
    public float fireRateSemi;
    [BoxGroup("Gun Info")]
    [ShowIf("fireTypes", FireType.burst)]public float burstRate;
    [BoxGroup("Gun Info")]
    public float range;
    [BoxGroup("Gun Info")]
    public List<FireType> fireTypes;
    [BoxGroup("Gun Info")]
    public GameObject bulletPrefab;
    [BoxGroup("Gun Info")]
    public GameObject bulletImpactPrefab;
}

public enum FireType
{
    semiAuto,
    burst,
    fullAuto
}
