using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [BoxGroup("Gunsmith")]
    public UIUtilities utilities;
    [BoxGroup("Gunsmith")]
    public GunsmithUI gunsmithUI;

    [BoxGroup("UI")]
    public CharacterItemInfo itemInfo;
    
    [BoxGroup("UI")]
    public GameObject defaultIcon;
    [BoxGroup("UI")]
    public Image itemIcon;
    [BoxGroup("UI")]
    public GameObject hoveredObject;
    [BoxGroup("UI")]
    public bool hovered;

    [BoxGroup("Items")]
    public InventoryItem item;

    public void Start()
    {
        
    }

    private void Update() {
        
        if(item.item){
            itemIcon.gameObject.SetActive(true);
            defaultIcon.SetActive(false);
            itemIcon.sprite = item.item.itemIcon;
        }else{
            itemIcon.gameObject.SetActive(false);
            defaultIcon.SetActive(true);
        }

        hoveredObject.SetActive(hovered);
        if(hovered){
            itemInfo.item = item.item;
            if(Input.GetMouseButtonDown(0)){
            }
            if(Input.GetKeyDown(KeyCode.R) && item.item != null){
                if(item.item.gunReference != null){
                    utilities.ChangeMenu(gunsmithUI.gameObject);
                    gunsmithUI.currentlyEditingGun = item;
                    gunsmithUI.FindGun(item);
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
