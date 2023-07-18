using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using Firebase.Auth;

public class PlayerController : HealthManager
{
    [BoxGroup("References")]
    public UserInfo info;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public GameObject camera;
    [BoxGroup("References")]
    public InputManager inputManager;
    [BoxGroup("References")]
    public PlayerMovement controller;
    [BoxGroup("References")]
    public PlayerCamera playerCamera;
    [BoxGroup("References")]
    public PlayerUIController uiController;
    [BoxGroup("References")]
    public Animator anim;
    [BoxGroup("References")]
    public AnimatorOverrideController overrideController;
    [BoxGroup("References")]
    public GunManager gunManager;

    [BoxGroup("Models")]
    public GameObject handModel;
    [BoxGroup("Models")]
    public GameObject bodyModel;

    [BoxGroup("Grenades")]
    public float grenadeEndTime;
    [BoxGroup("Grenades")]
    public GameObject grenadePrefab;
    [BoxGroup("Grenades")]
    public Transform grenadePoint;
    [BoxGroup("Grenades")]
    public bool canGrenade = true;
    [BoxGroup("Grenades")]
    public int amountOfGrenades = 3;

    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        if(view.IsMine){
            bodyModel.SetActive(false);
            info = UserManager.instance.userInfo;
            view.Owner.NickName = info.displayName;
            //grenadePoint = GetComponentInChildren<GrenadePoint>().transform;
        }else{
            Destroy(handModel);
            bodyModel.SetActive(true);
            camera.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!view.IsMine)
            return;

        if(!gunManager){
            gunManager = GetComponentInChildren<GunManager>();
        }

        if(inputManager.pauseValue){
            if(!paused){
                paused = true;
                playerCamera.disabled = true;
                controller.disabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                uiController.pauseMenu.SetActive(true);
            }else{
                paused = false;
                playerCamera.disabled = false;
                controller.disabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                uiController.pauseMenu.SetActive(false);
            }
            inputManager.pauseValue = false;
        }
        
        if(!paused){
            if(inputManager.grenadeValue && canGrenade && amountOfGrenades > 0){
                StartCoroutine(GrenadeThrow());
                inputManager.grenadeValue = false;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        transform.position = new Vector3(0, 2, 0);
    }

    public IEnumerator GrenadeThrow()
    {
        anim.SetLayerWeight(2, 1);
        canGrenade = false;
        overrideController.runtimeAnimatorController.animationClips[2] = gunManager.gunItem.idleAnim;
        anim.SetBool("Grenade", true);
        yield return new WaitForSeconds(grenadeEndTime);
        anim.SetBool("Grenade", false);
        canGrenade = true;
        amountOfGrenades--;
        PhotonNetwork.Instantiate("Grenade", grenadePoint.position, camera.transform.rotation);
    }

    public void Unpause()
    {        
        paused = false;
        playerCamera.disabled = false;
        controller.disabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        uiController.pauseMenu.SetActive(false);
    } 
}
