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
    [BoxGroup("Basic Info")]
    public ItemType itemType;
    [BoxGroup("Basic Info")]
    public int itemID;
    
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

[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;
    
    [BoxGroup("Weapons")]
    public AttachmentItem scope;
    [BoxGroup("Weapons")]
    public AttachmentItem frontGrip;
    [BoxGroup("Weapons")]
    public AttachmentItem grip;
    [BoxGroup("Weapons")]
    public AttachmentItem side;
    [BoxGroup("Weapons")]
    public AttachmentItem barrel;

    public InventoryItem(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

}

[System.Serializable]
public class SaveInventoryItem
{
    public int id;
    public int amount;
    public SaveInventoryItem(int _id, int _amount)
    {
        id = _id;
        amount = _amount;
    }
}

public enum ItemType
{
    basic,
    head,
    chest,
    legs,
    feet,
    wrist,
    back,
    gun,
    melee,
    grenade,
    equipment,
    attachment,
    bullet
}
