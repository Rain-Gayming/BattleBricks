using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

public class GunManager : MonoBehaviour
{
    [BoxGroup("References")]
    public Animator anim;
    [BoxGroup("References")]
    public PlayerController playerController;
    [BoxGroup("References")]
    public PlayerMovement playerMovement;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public GunItem gunItem;
    [BoxGroup("References")]
    public BulletItem bulletItem;
    [BoxGroup("References")]
    public InputManager inputManager;

    [BoxGroup("Gun")]
    public FireType currentFireType;
    [BoxGroup("Gun")]
    public Transform shootPoint;
    [BoxGroup("Gun")]
    public bool aiming;
    float shotTime;
    [BoxGroup("Gun")]
    public int ammo;
    
    [BoxGroup("Settings")]
    public bool toggleAim;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponentInParent<PhotonView>();
        if(!view.IsMine){
            Destroy(this);
        }else{
            playerController = GetComponentInParent<PlayerController>();
            playerMovement = GetComponentInParent<PlayerMovement>();
            anim = GetComponentInParent<Animator>();
            inputManager = GetComponentInParent<InputManager>();
            ammo = gunItem.maxAmmo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        shotTime -= Time.deltaTime;
        anim.SetLayerWeight(gunItem.animLayer, 1);
        if(!playerMovement.disabled){
            
            if(inputManager.reloadValue){
                StartCoroutine(Reload());
            }

            if(inputManager.shootValue && shotTime <= 0 && ammo > 0){
                switch (currentFireType)
                {
                    case FireType.semiAuto:
                        shotTime = gunItem.fireRateSemi;
                        SingleShot();
                    break;       
                    case FireType.burst:
                        StartCoroutine(BurstShoot());
                    break;       
                    case FireType.fullAuto:
                        shotTime = gunItem.fireRateAuto;
                        Shoot();                
                    break;                  
                }
            }

            if(toggleAim){
                if(inputManager.aimValue){
                    if(aiming){
                        aiming = false;
                        inputManager.aimValue = false;
                    }else{
                        aiming = true;
                        inputManager.aimValue = false;
                    }
                }
            }else{
                aiming = inputManager.aimValue;
            }

            if(inputManager.changeFireModeValue){
                
                switch (currentFireType)
                {
                    case FireType.semiAuto: 
                        if(gunItem.fireTypes.Contains(FireType.burst)){
                            currentFireType = FireType.burst;
                        }
                        else if(gunItem.fireTypes.Contains(FireType.fullAuto)){
                            currentFireType = FireType.fullAuto;
                        }
                    break;
                    case FireType.burst: 
                        if(gunItem.fireTypes.Contains(FireType.fullAuto)){
                            currentFireType = FireType.fullAuto;
                        }
                        else if(gunItem.fireTypes.Contains(FireType.semiAuto)){
                            currentFireType = FireType.semiAuto;
                        }
                    break;
                    case FireType.fullAuto: 
                        if(gunItem.fireTypes.Contains(FireType.semiAuto)){
                            currentFireType = FireType.semiAuto;
                        }
                        else if(gunItem.fireTypes.Contains(FireType.burst)){
                            currentFireType = FireType.burst;
                        }
                    break;
                }

                inputManager.changeFireModeValue = false;
            }
            anim.SetBool("Aiming", aiming);
        }
    }

    public IEnumerator Reload()
    {
        anim.SetBool("Reload", true);
        yield return new WaitForSeconds(gunItem.reloadTime);
        anim.SetBool("Reload", false);
        ammo = gunItem.maxAmmo;
    }

    public IEnumerator BurstShoot()
    {
        SingleShot();
        yield return new WaitForSeconds(gunItem.burstRate);
        SingleShot();
        yield return new WaitForSeconds(gunItem.burstRate);
        SingleShot();
    }

    public void SingleShot()
    {
        inputManager.shootValue = false;
        PhotonNetwork.Instantiate(Path.Combine("Bullets",  bulletItem.bulletName), shootPoint.position, shootPoint.rotation);
        ammo--;
    }

    public void Shoot()
    {
        PhotonNetwork.Instantiate(Path.Combine("Bullets",  bulletItem.bulletName), shootPoint.position, shootPoint.rotation);
        ammo--;
    }
}
