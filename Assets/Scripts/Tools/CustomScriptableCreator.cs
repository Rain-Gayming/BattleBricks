using System.Collections;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

public class ScriptableCreator : OdinMenuEditorWindow
{
    [MenuItem("Tools/ItemEditor")]
    public static void OpenWindow()
    {
        GetWindow<ScriptableCreator>().Show();
    }

    private CreateNewItem createNewItem;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        
        createNewItem = new CreateNewItem();
        tree.Add("Create New Item", createNewItem);
        tree.AddAllAssetsAtPath("Items", "Assets/ScriptableObjects/Items/Items", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Guns", "Assets/ScriptableObjects/Items/ItemReferences/Guns", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Back", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Back", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Chest", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Chest", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Feet", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Feet", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Head", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Legs", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Legs", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Armour/Wrist", "Assets/ScriptableObjects/Items/ItemReferences/Armour/Wrist", typeof(ItemObject));

        tree.AddAllAssetsAtPath("Items/References/Attachments/Barrels", "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Barrels", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Attachments/Front Grips", "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Front Grips", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Attachments/Grips", "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Grips", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Attachments/Scopes", "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Scopes", typeof(ItemObject));
        tree.AddAllAssetsAtPath("Items/References/Attachments/Side", "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Side", typeof(ItemObject));

        tree.AddAllAssetsAtPath("Items/References/Bullets", "Assets/ScriptableObjects/Items/ItemReferences/Bullets", typeof(ItemObject));

        
        tree.AddAllAssetsAtPath("Weapons", "Assets/ScriptableObjects/Items/Weapons", typeof(WeaponItem));
        tree.AddAllAssetsAtPath("Guns", "Assets/ScriptableObjects/Items/Guns", typeof(GunItem));
        tree.AddAllAssetsAtPath("Bullets", "Assets/ScriptableObjects/Items/Bullets", typeof(BulletItem));
        tree.AddAllAssetsAtPath("Attachments/Barrels", "Assets/ScriptableObjects/Items/Attachments/Barrels", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Front Grips", "Assets/ScriptableObjects/Items/Attachments/Front Grips", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Grips", "Assets/ScriptableObjects/Items/Attachments/Grips", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Scopes", "Assets/ScriptableObjects/Items/Attachments/Scopes", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Side", "Assets/ScriptableObjects/Items/Attachments/Side", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Armour", "Assets/ScriptableObjects/Items/Armour", typeof(ArmourItem));

        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        OdinMenuTreeSelection selected = this.MenuTree.Selection;


        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if(SirenixEditorGUI.ToolbarButton("Delete Current")){
                ItemObject asset = selected.SelectedValue as ItemObject;
                string basePath = AssetDatabase.GetAssetPath(asset);
                string refPath = "";
                if(asset.bulletReference){
                    refPath = AssetDatabase.GetAssetPath(asset.bulletReference);
                }
                if(asset.weaponReference){
                    refPath = AssetDatabase.GetAssetPath(asset.weaponReference);
                }
                if(asset.gunReference){
                    refPath = AssetDatabase.GetAssetPath(asset.gunReference);
                }
                if(asset.attachmentReference){
                    refPath = AssetDatabase.GetAssetPath(asset.attachmentReference);
                }
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.baseItem));
                AssetDatabase.DeleteAsset(basePath);
                AssetDatabase.DeleteAsset(refPath);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    public class CreateNewItem
    {
        public ItemDatabase itemDatabase;
#region  items
        [HideInEditorMode] public ItemObject itemObject;
        [HideInEditorMode] public WeaponItem weaponItem;
        [HideInEditorMode] public GunItem gunItem;
        [HideInEditorMode] public BulletItem bulletItem;
        [HideInEditorMode] public AttachmentItem attachmentItem;
        [HideInEditorMode] public ArmourItem armourItem;
        [HideInEditorMode] public GrenadeItem grenadeItem;
#endregion
        
        [BoxGroup("Item")]
        public ItemType itemType; 
        [BoxGroup("Item")]
        [TextArea(5, 10)]
        public string itemDescription;
        [BoxGroup("Item")]
        public bool equippable;

        
        [HorizontalGroup("Item/Display Data")]
        [PreviewField(75, Sirenix.OdinInspector.ObjectFieldAlignment.Left)]
        [HideLabel]
        public Sprite itemIcon;

        [HorizontalGroup("Item/Display Data/Name")]
        [LabelWidth(100)]
        public string itemName;

#region Gun Info

        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        public int animLayer;
        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        public float fireRateAuto;
        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        public float fireRateSemi;
        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        [ShowIf("fireTypes", FireType.burst)] public float burstRate;
        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        public float range;
        [BoxGroup("Gun Info")] [ShowIf("itemType", ItemType.gun)]
        public List<FireType> fireTypes;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AudioPack shootPack;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AttachmentPack scopeAttachmentPack;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AttachmentPack barrelAttachmentPack;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AttachmentPack gripAttachmentPack;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AttachmentPack frontGripAttachmentPack;
        [BoxGroup("Gun Info/Audio")] [ShowIf("itemType", ItemType.gun)]
        public AttachmentPack sideAttachmentPack;

        [BoxGroup("Gun Info/Sway")][ShowIf("itemType", ItemType.gun)]
        public float weight;

        [BoxGroup("Gun Info/Recoil")][ShowIf("itemType", ItemType.gun)]
        public float recoilX;
        [BoxGroup("Gun Info/Recoil")][ShowIf("itemType", ItemType.gun)]
        public float recoilY;
        [BoxGroup("Gun Info/Recoil")][ShowIf("itemType", ItemType.gun)]
        public float recoilZ;
        
        [BoxGroup("Gun Info/Aiming Recoil")][ShowIf("itemType", ItemType.gun)]
        public float aimRecoilX;
        [BoxGroup("Gun Info/Aiming Recoil")][ShowIf("itemType", ItemType.gun)]
        public float aimRecoilY;
        [BoxGroup("Gun Info/Aiming Recoil")][ShowIf("itemType", ItemType.gun)]
        public float aimRecoilZ;

#endregion

#region Bullet Info

        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public float velocity;
        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public float damage;
        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public Caliber caliber;
#endregion

#region  Attachment Item

        [BoxGroup("Attachment Info")] [ShowIf("itemType", ItemType.attachment)]
        public AttachmentType attachmentType;
        [BoxGroup("Attachment Info")] [ShowIf("itemType", ItemType.attachment)]
        public AttachmentPack attachmentPack;
#endregion

#region  Armour Item

        [BoxGroup("Armour Info")] [ShowIf("itemType", ItemType.armour)]
        public int armourValue;
        [BoxGroup("Armour Info")] [ShowIf("itemType", ItemType.armour)]
        public int radiationProtection;
        [BoxGroup("Armour Info")] [ShowIf("itemType", ItemType.armour)]
        public int extraSlots;
        [BoxGroup("Armour Info")] [ShowIf("itemType", ItemType.armour)]
        public ArmourType armourType;
        

#endregion

        [BoxGroup("Grenade Info")] [ShowIf("itemType", ItemType.grenade)]
        public int grenadeDamage;
        [BoxGroup("Grenade Info")] [ShowIf("itemType", ItemType.grenade)]
        public int grenadeVelocity;
        [BoxGroup("Grenade Info")] [ShowIf("itemType", ItemType.grenade)]
        public int grenadeFragments;

#region  Grenade Item


#endregion
        public CreateNewItem()
        {
            itemDatabase = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Items/Item Database.asset", typeof(ItemDatabase)) as ItemDatabase;
            
            itemObject = ScriptableObject.CreateInstance<ItemObject>();
            itemObject.isBase = true;
            itemObject.itemName = itemName;
            itemObject.itemDescription = itemDescription;
            itemObject.baseItem = itemObject;
            switch (itemType)
            {
                case ItemType.melee:
                    weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
                    itemObject.weaponReference = weaponItem;
                    weaponItem.itemName = itemName;
                    weaponItem.itemDescription = itemDescription;
                    weaponItem.baseItem = itemObject;
                break;
                case ItemType.gun:
                    gunItem = ScriptableObject.CreateInstance<GunItem>();
                    itemObject.gunReference = gunItem;
                    gunItem.itemName = itemName;
                    gunItem.itemDescription = itemDescription;
                    gunItem.baseItem = itemObject;
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    itemObject.bulletReference = bulletItem;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;
                    bulletItem.baseItem = itemObject;
                break;
                case ItemType.attachment:
                    attachmentItem = ScriptableObject.CreateInstance<AttachmentItem>();
                    itemObject.attachmentReference = attachmentItem;
                    attachmentItem.attachmentType = attachmentType;
                    attachmentItem.baseItem = itemObject;
                break;
            }
        }

        [Button("Make New Item")]
        public void CreateNewItemData()
        {
            itemObject = ScriptableObject.CreateInstance<ItemObject>();
            itemObject.isBase = true;
            itemObject.itemName = itemName;
            itemObject.itemDescription = itemDescription;
            itemObject.itemIcon = itemIcon;
            itemObject.itemType = itemType;
            itemObject.equippable = equippable;

            switch (itemType)
            {
                case ItemType.melee:
                    weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
                    itemObject.weaponReference = weaponItem;
                    weaponItem.itemType = itemType;
                    weaponItem.itemName = itemName;
                    weaponItem.itemDescription = itemDescription;
                    weaponItem.baseItem = itemObject;

                    itemDatabase.items.Add(weaponItem);
                break;
                case ItemType.gun:
                    gunItem = ScriptableObject.CreateInstance<GunItem>();
                    itemObject.gunReference = gunItem;
                    gunItem.itemType = itemType;
                    gunItem.itemName = itemName;
                    gunItem.itemDescription = itemDescription;

                    gunItem.animLayer = animLayer;
                    gunItem.fireRateAuto = fireRateAuto;
                    gunItem.fireRateSemi = fireRateSemi;
                    gunItem.burstRate = burstRate;
                    gunItem.range = range;
                    gunItem.shootPack = shootPack;
                    gunItem.baseItem = itemObject;
                    
                    gunItem.scopeAttachmentPack = scopeAttachmentPack;
                    gunItem.barrelAttachmentPack = barrelAttachmentPack;
                    gunItem.gripAttachmentPack = gripAttachmentPack;
                    gunItem.frontGripAttachmentPack = frontGripAttachmentPack;
                    gunItem.sideAttachmentPack = sideAttachmentPack;

                    gunItem.weight = weight;

                    gunItem.recoilX = recoilX;
                    gunItem.recoilY = recoilY;
                    gunItem.recoilZ = recoilZ;

                    gunItem.aimRecoilX = aimRecoilX;
                    gunItem.aimRecoilY = aimRecoilY;
                    gunItem.aimRecoilZ = aimRecoilZ;

                    itemDatabase.items.Add(gunItem);
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    itemObject.bulletReference = bulletItem;
                    bulletItem.itemType = itemType;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;
                    bulletItem.baseItem = itemObject;

                    bulletItem.damage = damage;
                    bulletItem.caliber = caliber;
                    bulletItem.velocity = velocity;
                    bulletItem.baseItem = itemObject;
                    
                    itemDatabase.items.Add(bulletItem);
                break;
                case ItemType.attachment:
                    attachmentItem = ScriptableObject.CreateInstance<AttachmentItem>();
                    itemObject.attachmentReference = attachmentItem;
                    attachmentItem.itemType = itemType;
                    attachmentItem.itemName = itemName;
                    attachmentItem.itemDescription = itemDescription;
                    attachmentItem.baseItem = itemObject;
                    
                    attachmentItem.attachmentType = attachmentType;
                    attachmentItem.baseItem = itemObject;
                    attachmentPack.attachments.Add(attachmentItem);
                    
                    itemDatabase.items.Add(attachmentItem);
                break;
                case ItemType.armour:
                    armourItem = ScriptableObject.CreateInstance<ArmourItem>();
                    itemObject.armourReference = armourItem;

                    armourItem.itemType = itemType;
                    armourItem.itemName = itemName;
                    armourItem.itemDescription = itemDescription;
                    armourItem.baseItem = itemObject;

                    armourItem.armourValue = armourValue;
                    armourItem.radiationProtection = radiationProtection;
                    armourItem.extraSlots = extraSlots;
                break; 
                case ItemType.grenade:
                    grenadeItem = ScriptableObject.CreateInstance<GrenadeItem>();
                    itemObject.grenadeReference = grenadeItem;
                    grenadeItem.baseItem = itemObject;

                    grenadeItem.damage = grenadeDamage;
                    grenadeItem.fragments = grenadeFragments;
                    grenadeItem.velocity = grenadeVelocity;
                break;
            }

            if(weaponItem){
                AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Weapons/ItemRefWeapon_" + itemName + ".asset");
                AssetDatabase.CreateAsset(weaponItem, "Assets/ScriptableObjects/Items/Weapons/Weapon_" + itemName + ".asset");
            }
            if(gunItem){
                AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Guns/ItemRefGun_" + itemName + ".asset");
                AssetDatabase.CreateAsset(gunItem, "Assets/ScriptableObjects/Items/Guns/Gun_" + itemName + ".asset");
            }
            if(bulletItem){
                AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Bullets/ItemRefBullet_" + itemName + ".asset");
                AssetDatabase.CreateAsset(bulletItem, "Assets/ScriptableObjects/Items/Bullets/Bullet_" + itemName + ".asset");
            }
            if(grenadeItem){
                AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Grenades/ItemRefGrenade_" + itemName + ".asset");
                AssetDatabase.CreateAsset(grenadeItem, "Assets/ScriptableObjects/Items/Grenades/Grenade_" + itemName + ".asset");
            }
            if(attachmentItem){
                switch (attachmentType)
                {
                    case AttachmentType.scope:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Scope/ItemRefAttachmentScope_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Scopes/AttachmentScope_" + itemName + ".asset");
                    break;
                    case AttachmentType.frontGrip:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Attachments/FrontGrip/ItemRefAttachmentFrontGrip_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Front Grips/AttachmentFrontGrip_" + itemName + ".asset");
                    break;
                    case AttachmentType.grip:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Grip/ItemRefAttachmentGrip_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Grips/AttachmentGrip_" + itemName + ".asset");
                    break;
                    case AttachmentType.side:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Side/ItemRefAttachmentSide_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Sides/AttachmentSide_" + itemName + ".asset");
                    break;
                    case AttachmentType.barrel:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Attachments/Barrels/ItemRefAttachmentBarrel_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Barrels/AttachmentBarrel_" + itemName + ".asset");
                    break;
                }
            }
            if(armourItem){
                switch (armourType)
                {
                    case ArmourType.head:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefHead_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Head/AHead_" + itemName + ".asset");
                    break;
                    case ArmourType.chest:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefChest_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Chest/ArmourChest_" + itemName + ".asset");
                    break;
                    case ArmourType.back:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefBack_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Back/ArmourBack_" + itemName + ".asset");
                    break;
                    case ArmourType.legs:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefLegs_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Legs/ArmourLegs_" + itemName + ".asset");
                    break;
                    case ArmourType.wrist:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefWrist_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Wrist/ArmourWrist_" + itemName + ".asset");
                    break;
                    case ArmourType.feet:
                        AssetDatabase.CreateAsset(itemObject, "Assets/ScriptableObjects/Items/ItemReferences/Armour/Head/ItemRefFeet_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Armour/Feet/ArmourFeet_" + itemName + ".asset");
                    break;
                }
            }
            AssetDatabase.SaveAssets();

            itemName = null;
            itemDescription = null;

            itemDatabase.items.Add(itemObject);
            itemDatabase.SetIDs();
        }
    }
}
#endif 