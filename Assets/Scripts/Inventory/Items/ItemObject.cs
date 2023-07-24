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
    [BoxGroup("Basic Info")]
    public bool equippable;

    [BoxGroup("Armour Info")][ShowIf("itemType", ItemType.armour)]
    public ArmourType armourType;
    
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
    [BoxGroup("References")]
    [ShowIf("isBase")] 
    public ArmourItem armourReference;
    [BoxGroup("References")]
    [ShowIf("isBase")] 
    public GrenadeItem grenadeReference;
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

    public InventoryItem(ItemObject _item, int _amount, AttachmentItem _scope, AttachmentItem _front, AttachmentItem _grip, AttachmentItem _side, AttachmentItem _barrel)
    {
        item = _item;
        amount = _amount;
        scope = _scope;
        frontGrip = _front;
        grip = _grip;
        side = _side;
        barrel = _barrel;
    }

}

[System.Serializable]
public class SaveInventoryItem
{
    public int id;
    public int amount;
    public int scopeID;
    public int frontID;
    public int gripID;
    public int sideID;
    public int barrelID;
    public SaveInventoryItem(int _id, int _amount, int _scopeID, int _frontID, int _gripID, int _sideID, int _barrelID)
    {
        id = _id;
        amount = _amount;
        scopeID = _scopeID;
        frontID = _frontID;
        gripID = _gripID;
        sideID = _sideID;
        barrelID = _barrelID;
    }
}

public enum ItemType
{
    basic,
    armour,
    gun,
    melee,
    grenade,
    equipment,
    attachment,
    bullet
}

public enum ArmourType
{    
    head,
    chest,
    legs,
    feet,
    wrist,
    back,
}