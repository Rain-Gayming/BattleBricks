using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

public class ItemDrag : MonoBehaviour
{
    public ItemSlot hoveredSlot;
    public ItemSlot fromSlot;
    public InventoryItem storedItem;
    public GameObject itemDragObject;
    public Image itemIcon;

    private void Update() 
    {
        itemDragObject.transform.position = Input.mousePosition;

        if(fromSlot){
            if(fromSlot.item != null){
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = fromSlot.item.item.itemIcon;
            }else{
                fromSlot = null;
            }
        }else{
            itemIcon.gameObject.SetActive(false);
        }
        if(Input.GetMouseButtonDown(0)){
            MousePressed();
        }
        
    }

    public void MousePressed()
    {
        if(fromSlot && fromSlot.item.amount > 0){
            if(hoveredSlot){
                if(!hoveredSlot.restricted){
                    storedItem = hoveredSlot.item;
                    hoveredSlot.item = fromSlot.item;
                    fromSlot.item = storedItem;

                    storedItem = null;
                    hoveredSlot = null;
                    fromSlot = null;
                }else{
                    if(hoveredSlot.restriction == fromSlot.item.item.itemType){
                        storedItem = hoveredSlot.item;
                        hoveredSlot.item = fromSlot.item;
                        fromSlot.item = storedItem;

                        storedItem = null;
                        hoveredSlot = null;
                        fromSlot = null;
                    }else{                        
                        storedItem = null;
                        hoveredSlot = null;
                        fromSlot = null;
                    }
                }
            }else if(!hoveredSlot){
                storedItem = fromSlot.item;
                GetComponent<PlayerInventory>().items.Remove(storedItem);
                GetComponent<PlayerInventory>().AddItem(storedItem);       

                fromSlot.item = new InventoryItem(null, 0, null, null, null, null, null);
                storedItem = null;
                hoveredSlot = null;
                fromSlot = null;        
            }
        }else{
            if(hoveredSlot && hoveredSlot.item.amount > 0)
                fromSlot = hoveredSlot;
        }
    }
}
