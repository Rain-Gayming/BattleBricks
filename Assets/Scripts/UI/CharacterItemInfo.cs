using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class CharacterItemInfo : MonoBehaviour
{
    public ItemObject item;

    [BoxGroup("Gun Info")]
    public GameObject gunArea;
    [BoxGroup("Gun Info")]
    public TMP_Text fireRateText;
    [BoxGroup("Gun Info")]
    public TMP_Text maxAmmoText;
    [BoxGroup("Gun Info")]
    public TMP_Text reloadTimeText;
    [BoxGroup("Gun Info")]
    public TMP_Text horizontalRecoilText;
    [BoxGroup("Gun Info")]
    public TMP_Text verticalrecoilText;
    
    [BoxGroup("Armour Info")]
    public GameObject armourArea;
    [BoxGroup("Armour Info")]
    public TMP_Text armourValueText;
    [BoxGroup("Armour Info")]
    public TMP_Text radiationProtectionText;
    [BoxGroup("Armour Info")]
    public TMP_Text extraSlotText;
    [BoxGroup("Armour Info")]
    //public TMP_Text durabilityText;

    private void Update()
    {
        if(item){
            switch (item.itemType)
            {
                case ItemType.gun:
                    gunArea.SetActive(true);
                    armourArea.SetActive(false);
                    fireRateText.text = "RPM: " + item.gunReference.fireRateAuto;
                    maxAmmoText.text = "Max Ammo: " + item.gunReference.maxAmmo;
                    reloadTimeText.text = "Reload Time: " + item.gunReference.reloadTime;
                    horizontalRecoilText.text = "Horizontal Recoil: " + -item.gunReference.recoilX;
                    verticalrecoilText.text = "Vertical Recoil: " + -item.gunReference.recoilY; 
                break;

                case ItemType.armour:
                    gunArea.SetActive(false);
                    armourArea.SetActive(true);
                    armourValueText.text = "Armour Value: " + item.armourReference.armourReference;
                    radiationProtectionText.text = "Radiation Protection: " + item.armourReference.radiationProtection;
                    extraSlotText.text = "Extra Slots: " + item.armourReference.extraSlots;
                break;
            }  
        }  
    }
}