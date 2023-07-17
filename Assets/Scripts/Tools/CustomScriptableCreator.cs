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
        tree.AddAllAssetsAtPath("Weapons", "Assets/ScriptableObjects/Items/Weapons", typeof(WeaponItem));
        tree.AddAllAssetsAtPath("Guns", "Assets/ScriptableObjects/Items/Guns", typeof(GunItem));
        tree.AddAllAssetsAtPath("Bullets", "Assets/ScriptableObjects/Items/Bullets", typeof(BulletItem));
        tree.AddAllAssetsAtPath("Attachments/Barrels", "Assets/ScriptableObjects/Items/Attachments/Barrels", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Front Grips", "Assets/ScriptableObjects/Items/Attachments/Front Grips", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Grips", "Assets/ScriptableObjects/Items/Attachments/Grips", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Scopes", "Assets/ScriptableObjects/Items/Attachments/Scopes", typeof(AttachmentItem));
        tree.AddAllAssetsAtPath("Attachments/Side", "Assets/ScriptableObjects/Items/Attachments/Side", typeof(AttachmentItem));

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
        public enum ItemType
        {
            item,
            weapon,
            gun,
            bullet,
            attachment
        }

#region  items
        [HideInEditorMode] public ItemObject ItemObject;
        [HideInEditorMode] public WeaponItem weaponItem;
        [HideInEditorMode] public GunItem gunItem;
        [HideInEditorMode] public BulletItem bulletItem;
        [HideInEditorMode] public AttachmentItem attachmentItem;
#endregion
        
        [BoxGroup("Item")]
        public ItemType itemType; 
        [BoxGroup("Item")]
        [TextArea(5, 10)]
        public string itemDescription;

        
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
        public CreateNewItem()
        {
            ItemObject = ScriptableObject.CreateInstance<ItemObject>();
            ItemObject.isBase = true;
            ItemObject.itemName = itemName;
            ItemObject.itemDescription = itemDescription;
            ItemObject.baseItem = ItemObject;
            switch (itemType)
            {
                case ItemType.weapon:
                    weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
                    ItemObject.weaponReference = weaponItem;
                    weaponItem.itemName = itemName;
                    weaponItem.itemDescription = itemDescription;
                    weaponItem.baseItem = ItemObject;
                break;
                case ItemType.gun:
                    gunItem = ScriptableObject.CreateInstance<GunItem>();
                    ItemObject.gunReference = gunItem;
                    gunItem.itemName = itemName;
                    gunItem.itemDescription = itemDescription;
                    gunItem.baseItem = ItemObject;
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    ItemObject.bulletReference = bulletItem;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;
                    bulletItem.baseItem = ItemObject;
                break;
                case ItemType.attachment:
                    attachmentItem = ScriptableObject.CreateInstance<AttachmentItem>();
                    ItemObject.attachmentReference = attachmentItem;
                    attachmentItem.attachmentType = attachmentType;
                    attachmentItem.baseItem = ItemObject;
                break;
            }
        }

        [Button("Make New Item")]
        public void CreateNewItemData()
        {
            ItemObject = ScriptableObject.CreateInstance<ItemObject>();
            ItemObject.isBase = true;
            ItemObject.itemName = itemName;
            ItemObject.itemDescription = itemDescription;
            ItemObject.itemIcon = itemIcon;
            switch (itemType)
            {
                case ItemType.weapon:
                    weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
                    ItemObject.weaponReference = weaponItem;
                    weaponItem.itemName = itemName;
                    weaponItem.itemDescription = itemDescription;
                    weaponItem.baseItem = ItemObject;
                break;
                case ItemType.gun:
                    gunItem = ScriptableObject.CreateInstance<GunItem>();
                    ItemObject.gunReference = gunItem;
                    gunItem.itemName = itemName;
                    gunItem.itemDescription = itemDescription;

                    gunItem.animLayer = animLayer;
                    gunItem.fireRateAuto = fireRateAuto;
                    gunItem.fireRateSemi = fireRateSemi;
                    gunItem.burstRate = burstRate;
                    gunItem.range = range;
                    gunItem.shootPack = shootPack;
                    gunItem.baseItem = ItemObject;
                    
                    gunItem.scopeAttachmentPack = scopeAttachmentPack;
                    gunItem.barrelAttachmentPack = barrelAttachmentPack;
                    gunItem.gripAttachmentPack = gripAttachmentPack;
                    gunItem.frontGripAttachmentPack = frontGripAttachmentPack;
                    gunItem.sideAttachmentPack = sideAttachmentPack;
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    ItemObject.bulletReference = bulletItem;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;
                    bulletItem.baseItem = ItemObject;

                    bulletItem.damage = damage;
                    bulletItem.caliber = caliber;
                    bulletItem.velocity = velocity;
                    bulletItem.baseItem = ItemObject;
                break;
                case ItemType.attachment:
                    attachmentItem = ScriptableObject.CreateInstance<AttachmentItem>();
                    ItemObject.attachmentReference = attachmentItem;
                    attachmentItem.itemName = itemName;
                    attachmentItem.itemDescription = itemDescription;
                    attachmentItem.baseItem = ItemObject;
                    
                    attachmentItem.attachmentType = attachmentType;
                    attachmentItem.baseItem = ItemObject;
                    attachmentPack.attachments.Add(attachmentItem);
                break;
            }

            if(!attachmentItem)
                AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/i" + itemType + "_" + itemName + ".asset");
            if(weaponItem)
                AssetDatabase.CreateAsset(weaponItem, "Assets/ScriptableObjects/Items/Weapons/w_" + itemName + ".asset");
            if(gunItem)
                AssetDatabase.CreateAsset(gunItem, "Assets/ScriptableObjects/Items/Guns/g_" + itemName + ".asset");
            if(bulletItem)
                AssetDatabase.CreateAsset(bulletItem, "Assets/ScriptableObjects/Items/Bullets/b_" + itemName + ".asset");
            if(attachmentItem){
                switch (attachmentType)
                {
                    case AttachmentType.scope:
                        AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/iAScope"  + "_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Scopes/AScope_" + itemName + ".asset");
                    break;
                    case AttachmentType.frontGrip:
                        AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/iAFrontGrip_"  + "_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Front Grips/AFrontGrip_" + itemName + ".asset");
                    break;
                    case AttachmentType.grip:
                        AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/iAGrip_"  + "_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Grips/AGrip_" + itemName + ".asset");
                    break;
                    case AttachmentType.side:
                        AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/iASide_"  + "_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Sides/ASide_" + itemName + ".asset");
                    break;
                    case AttachmentType.barrel:
                        AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/iABarrel"  + "_" + itemName + ".asset");
                        AssetDatabase.CreateAsset(attachmentItem, "Assets/ScriptableObjects/Items/Attachments/Barrels/ABarrel" + itemName + ".asset");
                    break;
                }
            }
            AssetDatabase.SaveAssets();

            itemName = null;
            itemDescription = null;
        }
    }
}
#endif 