using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using Sirenix.OdinInspector;
using ES3Internal;
using TMPro;

public class EquipmentInventory : MonoBehaviour
{
    string saveFile;
    public ItemDatabase itemDatabase;
    [BoxGroup("Item Slot")]
    public GameObject itemSlot;
    [BoxGroup("Item Slot")]
    public List<GameObject> itemSlots;
    [BoxGroup("Item Slot")]
    public List<InventoryItem> items;
    [BoxGroup("Item Slot")]
    public InventorySaveFile inventorySaveFile;

    [BoxGroup("Grids/Weapons")]
    public GameObject gunGrid;
    [BoxGroup("Grids/Weapons")]
    public GameObject attachmentGrid;
    [BoxGroup("Grids/Weapons")]
    public GameObject meleeGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject headGrid;
    [BoxGroup("Grids/Weapons")]
    public GameObject grenadeGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject chestGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject legsGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject feetGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject backGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject wristsGrid;
    [BoxGroup("Grids")]
    public GameObject equipmentGrid;

    
    [BoxGroup("Test")]
    public InventoryItem testItem;

    AttachmentItem scope;
    AttachmentItem front;
    AttachmentItem grip;
    AttachmentItem side;
    AttachmentItem barrel;
    int scopeID;
    int frontID;
    int gripID;
    int sideID;
    int barrelID;

    public bool scopeFound;
    public bool frontFound;
    public bool gripFound;
    public bool sideFound;
    public bool barrelFound;

    [Button("AddItem")]
    public void TestAddItem()
    {
        AddItem(testItem);
    }
    private void Start() {
        saveFile = Application.persistentDataPath + "/" + "Inventory" + ".json";
        inventorySaveFile = new InventorySaveFile();
    }


    public void AddItem(InventoryItem item)
    {
        if(item.amount <= 0){
            item.amount = 1;
        }
        GameObject newItem = Instantiate(itemSlot);
        newItem.GetComponent<ItemSlot>().item = item;
        items.Add(item);
        if(item.item.itemType != ItemType.armour){
            switch (item.item.itemType)
            {
                case ItemType.gun:
                    newItem.transform.SetParent(gunGrid.transform);
                break;
                case ItemType.melee:
                    newItem.transform.SetParent(meleeGrid.transform);
                break;
                case ItemType.grenade:
                    newItem.transform.SetParent(grenadeGrid.transform);
                break;
                case ItemType.equipment:
                    newItem.transform.SetParent(equipmentGrid.transform);
                break;
                case ItemType.attachment:
                    newItem.transform.SetParent(attachmentGrid.transform);
                break;
            }
        }else{
            switch (item.item.armourType)
            {
                case ArmourType.head:
                    newItem.transform.SetParent(headGrid.transform);
                break;
                case ArmourType.chest:
                    newItem.transform.SetParent(chestGrid.transform);
                break;
                case ArmourType.legs:
                    newItem.transform.SetParent(legsGrid.transform);
                break;
                case ArmourType.feet:
                    newItem.transform.SetParent(feetGrid.transform);
                break;
                case ArmourType.back:
                    newItem.transform.SetParent(backGrid.transform);
                break;
                case ArmourType.wrist:
                    newItem.transform.SetParent(wristsGrid.transform);
                break;
            }
        }
        newItem.transform.localScale = Vector3.one;
    }

    [Button("Save Inventory")]
    public void SaveInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].scope){
                scopeID = items[i].scope.baseItem.itemID;
            }
            if(items[i].frontGrip){
                frontID = items[i].frontGrip.baseItem.itemID;
            }
            if(items[i].grip){
                gripID = items[i].grip.baseItem.itemID;
            }
            if(items[i].side){
                sideID = items[i].side.baseItem.itemID;
            }
            if(items[i].barrel){
                barrelID = items[i].barrel.baseItem.itemID;
            }
            inventorySaveFile.saveItems.Add(new SaveInventoryItem(items[i].item.baseItem.itemID, items[i].amount, scopeID, frontID, gripID, sideID, barrelID));
        }

        Debug.Log(saveFile);
        PlayerPrefs.SetString("Most Recent Save", saveFile);
        string jsonString = JsonUtility.ToJson(inventorySaveFile, true);

        File.WriteAllText(saveFile, jsonString);
    }
    
    [Button("Load Inventory")]
    public void LoadInventory()
    {
        string fileContents = File.ReadAllText(saveFile);
        inventorySaveFile = JsonUtility.FromJson<InventorySaveFile>(fileContents);


        for (int i = 0; i < inventorySaveFile.saveItems.Count; i++)
        {   
            Debug.Log(inventorySaveFile.saveItems[i].scopeID);
            Debug.Log(inventorySaveFile.saveItems[i].frontID);
            Debug.Log(inventorySaveFile.saveItems[i].gripID);
            Debug.Log(inventorySaveFile.saveItems[i].sideID);
            Debug.Log(inventorySaveFile.saveItems[i].barrelID);
            
            if(inventorySaveFile.saveItems[i].scopeID == 0){
                scopeFound = true;
            }
            if(inventorySaveFile.saveItems[i].frontID == 0){
                frontFound = true;
            }
            if(inventorySaveFile.saveItems[i].gripID == 0){
                gripFound = true;
            }
            if(inventorySaveFile.saveItems[i].sideID == 0){
                sideFound = true;
            }
            if(inventorySaveFile.saveItems[i].barrelID == 0){
                barrelFound = true;
            }

            for (int a = 0; a < itemDatabase.items.Count; a++)
            {
                if(itemDatabase.items[i].itemType != ItemType.basic && itemDatabase.items[i].itemType != ItemType.bullet){
                    if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].scopeID && !scopeFound){
                        scope = itemDatabase.items[a].attachmentReference;
                        scopeFound = true;
                    }
                    
                    if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].frontID && !frontFound){
                        front = itemDatabase.items[a].attachmentReference;
                        frontFound = true;
                    }
                    if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].gripID && !gripFound){
                        grip = itemDatabase.items[a].attachmentReference;
                        gripFound = true;
                    }
                    
                    if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].sideID && !sideFound){
                        side = itemDatabase.items[a].attachmentReference;
                        sideFound = true;
                    }
                    
                    if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].barrelID && !barrelFound){
                        barrel = itemDatabase.items[a].attachmentReference;
                        barrelFound = true;
                    }
                    if(scopeFound && frontFound && gripFound && side && barrelFound){
                        if(itemDatabase.items[a].itemID == inventorySaveFile.saveItems[i].id)
                            AddItem(new InventoryItem(itemDatabase.items[a], inventorySaveFile.saveItems[i].amount, scope, front, grip, side, barrel));
                    }                
                }
            }
        }
    }
}
