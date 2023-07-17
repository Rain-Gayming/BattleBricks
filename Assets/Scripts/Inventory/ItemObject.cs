using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemObject : ScriptableObject
{
    [BoxGroup("Basic Info")]
    public string itemName;
    [BoxGroup("Basic Info")]
    [TextArea(5, 10)]
    public string itemDescription;
    [BoxGroup("Basic Info")]
    public Sprite itemIcon;
    
    [BoxGroup("References")]
    [HideInInspector]public bool isBase;
    [BoxGroup("References")]
    public ItemObject baseItem;
    [BoxGroup("References")]
    [ShowIf("isBase")] public WeaponItem weaponReference;
    [BoxGroup("References")]
    [ShowIf("isBase")]  public GunItem gunReference;
    [BoxGroup("References")]
    [ShowIf("isBase")] public BulletItem bulletReference;
    [BoxGroup("References")]
    [ShowIf("isBase")] public AttachmentItem attachmentReference;
}
