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
            bullet
        }

#region  items
        [HideInEditorMode] public ItemObject ItemObject;
        [HideInEditorMode] public WeaponItem weaponItem;
        [HideInEditorMode] public GunItem gunItem;
        [HideInEditorMode] public BulletItem bulletItem;
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

#endregion

#region Bullet Info

        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public float velocity;
        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public float damage;
        [BoxGroup("Bullet Info")] [ShowIf("itemType", ItemType.bullet)]
        public Caliber caliber;
#endregion
        public CreateNewItem()
        {
            ItemObject = ScriptableObject.CreateInstance<ItemObject>();
            ItemObject.isBase = true;
            ItemObject.itemName = itemName;
            ItemObject.itemDescription = itemDescription;
            switch (itemType)
            {
                case ItemType.weapon:
                    weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
                    ItemObject.weaponReference = weaponItem;
                    weaponItem.itemName = itemName;
                    weaponItem.itemDescription = itemDescription;
                break;
                case ItemType.gun:
                    gunItem = ScriptableObject.CreateInstance<GunItem>();
                    ItemObject.gunReference = gunItem;
                    gunItem.itemName = itemName;
                    gunItem.itemDescription = itemDescription;
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    ItemObject.bulletReference = bulletItem;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;
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
                break;
                case ItemType.bullet:
                    bulletItem = ScriptableObject.CreateInstance<BulletItem>();
                    ItemObject.bulletReference = bulletItem;
                    bulletItem.itemName = itemName;
                    bulletItem.itemDescription = itemDescription;

                    bulletItem.damage = damage;
                    bulletItem.caliber = caliber;
                    bulletItem.velocity = velocity;
                break;
            }

            AssetDatabase.CreateAsset(ItemObject, "Assets/ScriptableObjects/Items/Items/i" + itemType + "_" + itemName + ".asset");
            if(weaponItem)
                AssetDatabase.CreateAsset(weaponItem, "Assets/ScriptableObjects/Items/Weapons/w_" + itemName + ".asset");
            if(gunItem)
                AssetDatabase.CreateAsset(gunItem, "Assets/ScriptableObjects/Items/Guns/g_" + itemName + ".asset");
            if(bulletItem)
                AssetDatabase.CreateAsset(bulletItem, "Assets/ScriptableObjects/Items/Bullets/b_" + itemName + ".asset");
            AssetDatabase.SaveAssets();

            itemName = null;
            itemDescription = null;
        }
    }
}
#endif 