using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmithUI : MonoBehaviour
{
    public AttachmentRenderer[] guns;
    public GrabRotation[] grabRotations;
    public InventoryItem currentlyEditingGun;
    public CharacterItemInfo characterItemInfo;

    public bool isSecondary;
    // Start is called before the first frame update
    void Start()
    {
        guns = FindObjectsOfType<AttachmentRenderer>();
        grabRotations = FindObjectsOfType<GrabRotation>();
    }
    public void ResetRotations()
    {
        for (int i = 0; i < grabRotations.Length; i++)
        {
            grabRotations[i].transform.rotation = Quaternion.identity;
        }
    }

    public void SaveGun()
    {
        if(!isSecondary){
            characterItemInfo.primarySlot.item = currentlyEditingGun;
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].gun.SetActive(false);
            }
        }else{

        }
    }

    public void FindGun(InventoryItem gunToFind)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if(guns[i].gunItem.item == gunToFind.item){
                guns[i].gameObject.SetActive(true);
                guns[i].gun.SetActive(true);
                Debug.Log("Gun found");
                if(gunToFind.scope)
                    guns[i].UpdateAttachment(gunToFind.scope);
                if(gunToFind.side)
                    guns[i].UpdateAttachment(gunToFind.side);
                if(gunToFind.frontGrip)
                    guns[i].UpdateAttachment(gunToFind.frontGrip);
                if(gunToFind.grip)
                guns[i].UpdateAttachment(gunToFind.grip);
                    if(gunToFind.barrel)
                    guns[i].UpdateAttachment(gunToFind.barrel);
                currentlyEditingGun = gunToFind;
            }else{
                guns[i].gun.SetActive(false);
            }
        }
    }
}
