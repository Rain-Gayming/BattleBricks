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
    [BoxGroup("Gun Info/Attachments")]
    public AttachmentPack scopeAttachmentPack;
    [BoxGroup("Gun Info/Attachments")]
    public AttachmentPack barrelAttachmentPack;
    [BoxGroup("Gun Info/Attachments")]
    public AttachmentPack gripAttachmentPack;
    [BoxGroup("Gun Info/Attachments")]
    public AttachmentPack frontGripAttachmentPack;
    [BoxGroup("Gun Info/Attachments")]
    public AttachmentPack sideAttachmentPack;
    [BoxGroup("Gun Info/Audio")]
    public AudioPack shootPack;
    [BoxGroup("Gun Info/Animation")]
    public AnimationClip idleAnim;
    [BoxGroup("Gun Info/Animation")]
    public AnimationClip sideGripAnim;
    [BoxGroup("Gun Info/Animation")]
    public int animLayer;
    
    [BoxGroup("Sway")]
    public float weight;

    [BoxGroup("Recoil")]
    public float recoilX;
    [BoxGroup("Recoil")]
    public float recoilY;
    [BoxGroup("Recoil")]
    public float recoilZ;

    [BoxGroup("Aiming Recoil")]
    public float aimRecoilX;
    [BoxGroup("Aiming Recoil")]
    public float aimRecoilY;
    [BoxGroup("Aiming Recoil")]
    public float aimRecoilZ;
}

public enum FireType
{
    semiAuto,
    burst,
    fullAuto
}
