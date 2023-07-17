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
    [BoxGroup("References")]
    public AnimatorOverrideController overrideController;

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
            
            anim.SetBool("SideGrip", hasSideGrip);
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
        //AudioManager.instance.PlayAudio(gunItem.shootPack.clips[Random.Range(0, gunItem.shootPack.clips.Count)].clipName, "Guns", false);
    }

    public void Shoot()
    {
        PhotonNetwork.Instantiate(Path.Combine("Bullets",  bulletItem.bulletName), shootPoint.position, shootPoint.rotation);
        ammo--;
    }
}
