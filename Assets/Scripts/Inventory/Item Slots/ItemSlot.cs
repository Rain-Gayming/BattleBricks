using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [BoxGroup("UI")]
    public bool destroy = true;
    [BoxGroup("UI")]
    public Image itemIcon;

    [BoxGroup("Items")]
    public InventoryItem item;
    [BoxGroup("Items")]
    public bool restricted;
    [BoxGroup("Items")]
    [ShowIf("restricted", true)]
    public ItemType restriction;

    private void Update() {
        if(!item.item && destroy){
            Destroy(gameObject);
        }else if(!item.item){
                itemIcon.gameObject.SetActive(false);
        }
        
        if(restricted && item.item){
            if(item.item.itemType == restriction){
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = item.item.itemIcon;
            }else{
                item = null;
            }
        }
    }
}
