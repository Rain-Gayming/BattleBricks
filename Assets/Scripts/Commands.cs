using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QFSW.QC;
using Photon.Pun;

public class Commands : MonoBehaviour
{
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public QuantumConsole console;
    [BoxGroup("References")]
    public PlayerController controller;
    [BoxGroup("References")]
    public PlayerMovement playerMovement;
    [BoxGroup("References")]
    public PlayerCamera playerCamera;
    [BoxGroup("References")]
    public PlayerUIController playerUI;

    private void Start()
    {
        if(!view.IsMine){
            Destroy(this);
        }    
    }

    public void Update()
    {
        if(console.IsActive){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.disabled = true;
            playerMovement.disabled = true;
        }else{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerCamera.disabled = false;
            playerMovement.disabled = false;
        }
    }

    [Command]
    public float Sensitivity(float x, float y)
    {
        playerCamera.mouseSensitvityX = x;
        return playerCamera.mouseSensitvityY = y;
    }
    
    [Command]
    public float SensitivityX(float x)
    {
        return playerCamera.mouseSensitvityX = x;
    }
    
    [Command]
    public float SensitivityY(float y)
    {
        return playerCamera.mouseSensitvityY = y;
    }

    [Command]
    public void ShowDebugText()
    {
        playerUI.speedText.gameObject.SetActive(!playerUI.speedText.gameObject.activeInHierarchy);
        playerUI.positionText.gameObject.SetActive(!playerUI.positionText.gameObject.activeInHierarchy);
    }
}
