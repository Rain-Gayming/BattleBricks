using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine.Audio;

public class GunManager : MonoBehaviour
{
    [BoxGroup("References")]
    public InputManager inputManager;
    [BoxGroup("References/Player")]
    public PlayerController playerController;
    [BoxGroup("References/Player")]
    public PlayerMovement playerMovement;
    [BoxGroup("References/Player")]
    public PlayerUIController playerUIController;
    
    [BoxGroup("References/Animations")]
    public PlayerAnimator playerAnimator;
    [BoxGroup("References/Animations")]
    public AnimatorOverrideController overrideController;
    [BoxGroup("References/Animations")]
    public Animator anim;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References/Weapons")]
    public GunItem gunItem;
    [BoxGroup("References/Weapons")]
    public BulletItem bulletItem;
    [BoxGroup("References/Weapons")]
    public WeaponSway weaponSway;
    [BoxGroup("References/Weapons")]
    public WeaponRecoil weaponRecoil;

    [BoxGroup("Gun")]
    public FireType currentFireType;
    [BoxGroup("Gun")]
    public Transform shootPoint;
    [BoxGroup("Gun")]
    public bool aiming;
    float shotTime;
    [BoxGroup("Gun")]
    public int ammo;
    [BoxGroup("Gun")]
    public bool hasSideGrip;
    [BoxGroup("Gun")]
    public Transform defaultBarrel;
    [BoxGroup("Gun")]
    public Transform magPoint;
    [BoxGroup("Gun")]
    public Transform gripPoint;

    [BoxGroup("Attachments")]
    [BoxGroup("Attachments/Scope")]
    public AttachmentItem scopeAttachment;
    [BoxGroup("Attachments/Scope")]
    public List<AttachmentObject> scopeAttachments;
    
    [BoxGroup("Attachments/Scope")]

    public AttachmentItem previousScopeAttachement;
    [BoxGroup("Attachments/Barrel")]
    public AttachmentItem barrelAttachment;
    [BoxGroup("Attachments/Barrel")]
    public List<AttachmentObject> barrelAttachments;
    [BoxGroup("Attachments/Barrel")]
    public AttachmentItem previousBarrelAttachement;

    [BoxGroup("Attachments/Grip")]
    public AttachmentItem gripAttachment;
    [BoxGroup("Attachments/Grip")]
    public List<AttachmentObject> gripAttachments;
    [BoxGroup("Attachments/Grip")]
    public AttachmentItem previousGripAttachement;
    
    [BoxGroup("Attachments/Front Grip")]
    public AttachmentItem frontGripAttachment;
    [BoxGroup("Attachments/Front Grip")]
    public List<AttachmentObject> frontGripAttachments;
    [BoxGroup("Attachments/Front Grip")]
    public AttachmentItem previousFrontGripAttachement;

    [BoxGroup("Attachments/Side")]
    public AttachmentItem sideAttachment;
    [BoxGroup("Attachments/Side")]
    public List<AttachmentObject> sideAttachments;
    [BoxGroup("Attachments/Side")]
    public AttachmentItem previousSideAttachement;
    
    [BoxGroup("Settings")]
    public bool toggleAim;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        view = GetComponentInParent<PhotonView>();
        if(!view.IsMine){
            weaponSway = GetComponentInParent<WeaponSway>();
            weaponRecoil = GetComponentInParent<WeaponRecoil>();
            Destroy(weaponSway);
            Destroy(weaponRecoil);
            Destroy(this);
        }else{
            playerController = GetComponentInParent<PlayerController>();
            playerMovement = GetComponentInParent<PlayerMovement>();
            playerUIController = GetComponentInParent<PlayerUIController>();

            inputManager = GetComponentInParent<InputManager>();
            anim = GetComponentInParent<Animator>();

            weaponSway = GetComponentInParent<WeaponSway>();
            weaponSway.currentGun = gunItem;

            weaponRecoil = GetComponentInParent<WeaponRecoil>();
            weaponRecoil.currentGun = gunItem;
            ammo = gunItem.maxAmmo;

            if(gripAttachment){
                for (int i = 0; i < gripAttachments.Count; i++)
                {
                    if(gripAttachments[i].attachmentItem == gripAttachment){
                        //playerAnimator.ChangeLeftPoint(gripAttachments[i].attachmentObject.transform);
                    }
                }
            }else{
                //playerAnimator.ChangeLeftPoint(defaultBarrel);
            }

            //playerAnimator.ChangeRightPoint(gripPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shotTime -= Time.deltaTime;
        if(!playerMovement.disabled){
            
            if(inputManager.reloadValue){
                StartCoroutine(Reload());
            }

            if(inputManager.shootValue && shotTime <= 0 && ammo > 0 && canShoot){
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
                        weaponSway.aiming = true;
                        weaponRecoil.aiming = true;
                    }else{
                        aiming = true;
                        inputManager.aimValue = false;
                        weaponSway.aiming = false;
                        weaponRecoil.aiming = false;
                    }
                }
            }else{
                aiming = inputManager.aimValue;
                weaponSway.aiming = inputManager.aimValue;
                weaponRecoil.aiming = inputManager.aimValue;
            }

            if(inputManager.changeFireModeValue){
                
                switch (currentFireType)
                {
                    case FireType.semiAuto: 
                        if(gunItem.fireTypes.Contains(FireType.burst)){
                            currentFireType = FireType.burst;
                            playerUIController.ChangeFireType(FireType.burst);
                        }
                        else if(gunItem.fireTypes.Contains(FireType.fullAuto)){
                            currentFireType = FireType.fullAuto;
                            playerUIController.ChangeFireType(FireType.fullAuto);
                        }
                    break;
                    case FireType.burst: 
                        if(gunItem.fireTypes.Contains(FireType.fullAuto)){
                            currentFireType = FireType.fullAuto;
                            playerUIController.ChangeFireType(FireType.fullAuto);
                        }
                        else if(gunItem.fireTypes.Contains(FireType.semiAuto)){
                            currentFireType = FireType.semiAuto;
                            playerUIController.ChangeFireType(FireType.semiAuto);
                        }
                    break;
                    case FireType.fullAuto: 
                        if(gunItem.fireTypes.Contains(FireType.semiAuto)){
                            currentFireType = FireType.semiAuto;
                            playerUIController.ChangeFireType(FireType.semiAuto);
                        }
                        else if(gunItem.fireTypes.Contains(FireType.burst)){
                            currentFireType = FireType.burst;
                            playerUIController.ChangeFireType(FireType.burst);
                        }
                    break;
                }

                //Changes attachments if theyre changed
                if(previousBarrelAttachement != barrelAttachment || previousFrontGripAttachement != frontGripAttachment || previousGripAttachement != gripAttachment || previousScopeAttachement != scopeAttachment || previousSideAttachement != sideAttachment){
                    for (int i = 0; i < barrelAttachments.Count; i++)
                    {
                        if(barrelAttachments[i].attachmentItem == barrelAttachment){
                            barrelAttachments[i].attachmentObject.SetActive(true);
                        }else{
                            barrelAttachments[i].attachmentObject.SetActive(false);
                        }
                    }
                    for (int i = 0; i < frontGripAttachments.Count; i++)
                    {
                        if(frontGripAttachments[i].attachmentItem == frontGripAttachment){
                            frontGripAttachments[i].attachmentObject.SetActive(true);
                        }else{
                            frontGripAttachments[i].attachmentObject.SetActive(false);
                        }
                    }
                    for (int i = 0; i < gripAttachments.Count; i++)
                    {
                        if(gripAttachments[i].attachmentItem == gripAttachment){
                            gripAttachments[i].attachmentObject.SetActive(true);
                        }else{
                            gripAttachments[i].attachmentObject.SetActive(false);
                        }
                    }
                    for (int i = 0; i < scopeAttachments.Count; i++)
                    {
                        if(scopeAttachments[i].attachmentItem == scopeAttachment){
                            scopeAttachments[i].attachmentObject.SetActive(true);
                        }else{
                            scopeAttachments[i].attachmentObject.SetActive(false);
                        }
                    }
                    for (int i = 0; i < sideAttachments.Count; i++)
                    {
                        if(sideAttachments[i].attachmentItem == sideAttachment){
                            sideAttachments[i].attachmentObject.SetActive(true);
                            GetComponent<Laser>().attachmentItem = sideAttachment;
                        }else{
                            sideAttachments[i].attachmentObject.SetActive(false);
                        }
                    }
                }
                inputManager.changeFireModeValue = false;
            }
            
            if(inputManager.checkAmmoValue){
                StartCoroutine(CheckAmmo());
            }
            anim.SetBool("Aiming", aiming);
        }
    }

    public IEnumerator CheckAmmo()
    {
        inputManager.checkAmmoValue = false;
        playerUIController.ammoUI.gameObject.SetActive(true);
        playerUIController.ammoText.text = ammo.ToString();
        yield return new WaitForSeconds(gunItem.reloadTime);
        playerUIController.ammoUI.gameObject.SetActive(false);
    }

    public IEnumerator Reload()
    {
        canShoot = false;
        anim.SetBool("Reload", true);
        yield return new WaitForSeconds(gunItem.reloadTime);
        anim.SetBool("Reload", false);
        if(gripAttachment){
            for (int i = 0; i < gripAttachments.Count; i++)
            {
                if(gripAttachments[i].attachmentItem == gripAttachment){
                }
            }
        }else{
        }
        ammo = gunItem.maxAmmo;
        canShoot = true;
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
        weaponRecoil.RecoilFire();
        //AudioManager.instance.PlayAudio(gunItem.shootPack.clips[Random.Range(0, gunItem.shootPack.clips.Count)].clipName, "Guns", false);
    }

    public void Shoot()
    {
        PhotonNetwork.Instantiate(Path.Combine("Bullets",  bulletItem.bulletName), shootPoint.position, shootPoint.rotation);
        ammo--;
        weaponRecoil.RecoilFire();
    }
}
