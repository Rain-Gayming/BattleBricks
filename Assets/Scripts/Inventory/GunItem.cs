using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GunItem : ItemObject
{
    [BoxGroup("Gun Info")]
    public float fireRateAuto;
    [BoxGroup("Gun Info")]
    public float fireRateSemi;
    [BoxGroup("Gun Info")]
    [ShowIf("fireTypes", FireType.burst)]public float burstRate;
    [BoxGroup("Gun Info")]
    public float range;
    [BoxGroup("Gun Info")]
    public int maxAmmo;
    [BoxGroup("Gun Info")]
    public float reloadTime;
    [BoxGroup("Gun Info")]
    public List<FireType> fireTypes;
    [BoxGroup("Gun Info/Audio")]
    public AudioPack shootPack;
    [BoxGroup("Gun Info/Animation")]
    public AnimationClip idleAnim;
    [BoxGroup("Gun Info/Animation")]
    public AnimationClip sideGripAnim;
    [BoxGroup("Gun Info/Animation")]
    public int animLayer;
}

public enum FireType
{
    semiAuto,
    burst,
    fullAuto
}
