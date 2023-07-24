using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public GunItem primaryGun;
    public ItemSlot primaryGunSlot;
    public GunItem secondaryGun;
    public ItemSlot secondaryGunSlot;

    void Start()
    {
        if(instance != null){
            Destroy(instance);
            instance = this;
        }else{
            instance = this;
        }
    }

    public void SwapPrimary(GunItem newItem, ItemSlot newSlot)
    {
        primaryGunSlot.equipped = false;
        primaryGun = newItem;
        primaryGunSlot = newSlot;
    }

    public void SwapSecondary(GunItem newItem, ItemSlot newSlot)
    {
        secondaryGunSlot.equipped = false;
        secondaryGun = newItem;
        secondaryGunSlot = newSlot;
    }



}
