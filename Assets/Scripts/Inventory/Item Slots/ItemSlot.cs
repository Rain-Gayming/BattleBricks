using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ItemDrag itemDrag;
    [BoxGroup("UI")]
    public bool destroy = true;
    [BoxGroup("UI")]
    public Image itemIcon;
    [BoxGroup("UI")]
    public GameObject hoveredObject;
    [BoxGroup("UI")]
    public bool hovered;
    [BoxGroup("UI")]
    public GameObject equippedObject;
    [BoxGroup("UI")]
    public bool equipped;

    [BoxGroup("Items")]
    public InventoryItem item;
    [BoxGroup("Items")]
    public bool restricted;
    [BoxGroup("Items")]
    [ShowIf("restricted", true)]
    public ItemType restriction;

    public void Start()
    {
        itemDrag = GetComponentInParent<ItemDrag>();
    }

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
                item = new InventoryItem(null, 0, null, null, null, null, null);
            }
        }

        hoveredObject.SetActive(hovered);
        equippedObject.SetActive(equipped);
        if(hovered){
            if(Input.GetMouseButtonDown(0)){
                if(item.item.equippable){
                    equipped = !equipped;
                    EquipmentManager.instance.SwapPrimary(item.item.gunReference, this);
                }
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

}
