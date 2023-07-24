using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerWeaponController : MonoBehaviour
{
    public InputManager inputManager;
    public PlayerAnimator playerAnimator;
    [BoxGroup("Weapons")]
    public GunItem primaryGun;
    [BoxGroup("Weapons")]
    public GunItem secondaryGun;
    [BoxGroup("Weapons")]
    public GameObject primaryObject;
    [BoxGroup("Weapons")]
    public GameObject secondaryObject;

    private void Update() {
        if(inputManager.primaryValue){
            inputManager.primaryValue = false;
            SwapGun(1);
        }
        if(inputManager.secondaryValue){
            inputManager.secondaryValue = false;
            SwapGun(2);
        }
    }

    
    public void SwapGun(int index)
    {
        if(index == 1){
            primaryObject.SetActive(true);
            primaryObject.GetComponent<GunManager>().gunItem = primaryGun;
            secondaryObject.SetActive(false);
            playerAnimator.ChangeLeftPoint(primaryObject.GetComponent<GunManager>().defaultBarrel);
            playerAnimator.ChangeRightPoint(primaryObject.GetComponent<GunManager>().gripPoint);
        }
        if(index == 2){
            primaryObject.SetActive(false);
            secondaryObject.GetComponent<GunManager>().gunItem = secondaryGun;
            secondaryObject.SetActive(true);
            playerAnimator.ChangeLeftPoint(primaryObject.GetComponent<GunManager>().defaultBarrel);
            playerAnimator.ChangeRightPoint(primaryObject.GetComponent<GunManager>().gripPoint);
        }
    }
}
