using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [BoxGroup("Item Slot")]
    public GameObject itemSlot;
    [BoxGroup("Item Slot")]
    public List<GameObject> itemSlots;
    [BoxGroup("Item Slot")]
    public List<InventoryItem> items;

    [BoxGroup("Grids")]
    public GameObject gunGrid;
    [BoxGroup("Grids")]
    public GameObject attachmentGrid;
    [BoxGroup("Grids")]
    public GameObject meleeGrid;
    [BoxGroup("Grids/Armour")]
    public GameObject headGrid;
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
    public GameObject bulletGrid;
    [BoxGroup("Grids")]
    public GameObject grenadeGrid;
    [BoxGroup("Grids")]
    public GameObject equipmentGrid;
    [BoxGroup("Grids")]
    public GameObject miscGrid;

    
    [BoxGroup("Test")]
    public InventoryItem testItem;

    [Button("AddItem")]
    public void TestAddItem()
    {
        AddItem(testItem);
    }


    public void AddItem(InventoryItem item)
    {
        GameObject newItem = Instantiate(itemSlot);
        newItem.GetComponent<ItemSlot>().item = item;
        switch (item.item.itemType)
        {
            case ItemType.basic:
                newItem.transform.SetParent(miscGrid.transform);
            break;
            case ItemType.head:
                newItem.transform.SetParent(headGrid.transform);
            break;
            case ItemType.chest:
                newItem.transform.SetParent(chestGrid.transform);
            break;
            case ItemType.legs:
                newItem.transform.SetParent(legsGrid.transform);
            break;
            case ItemType.feet:
                newItem.transform.SetParent(feetGrid.transform);
            break;
            case ItemType.wrist:
                newItem.transform.SetParent(wristsGrid.transform);
            break;
            case ItemType.back:
                newItem.transform.SetParent(backGrid.transform);
            break;
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
            case ItemType.bullet:
                newItem.transform.SetParent(bulletGrid.transform);
            break;
        }
        newItem.transform.localScale = Vector3.one;
    }
}
