using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerCamera : MonoBehaviour
{
    [BoxGroup("References")]
    public InputManager inputManager;
    [BoxGroup("References")]
    public Transform playerBody;
    [BoxGroup("References")]
    public bool disabled;
    [BoxGroup("References")]
    public PlayerUIController uiController;

    [BoxGroup("Interaction")]
    public float interactionRange;
    [BoxGroup("Interaction")]
    public Transform interactPoint;
    int selectionIndex;
    RaycastHit preInteractionHit;


    [BoxGroup("Sensitvity")]
    public float mouseSensitvityY;
    [BoxGroup("Sensitvity")]
    public float mouseSensitvityX;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!disabled){
            float mouseX = inputManager.lookValue.x * mouseSensitvityX * Time.deltaTime;
            float mouseY = inputManager.lookValue.y * mouseSensitvityY * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80, 80);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            RaycastHit interactionHit;
            if(Physics.Raycast(interactPoint.position, interactPoint.forward, out interactionHit,interactionRange)){
                if(interactionHit.transform.GetComponent<Interactable>()){

                    uiController.interactionUI.SetActive(true);
                    if(interactionHit.transform != preInteractionHit.transform){
                        Debug.Log("New Interactable Hit");
                        preInteractionHit = interactionHit;
                        uiController.ClearInteractions();
                        for (int i = 0; i < interactionHit.transform.GetComponent<Interactable>().interactions.Count; i++)
                        {
                            uiController.AddNewInteraction(interactionHit.transform.GetComponent<Interactable>().interactions[i]);
                        }
                    }
                    
                    
                    selectionIndex += Mathf.RoundToInt(-Input.mouseScrollDelta.y);
                    selectionIndex = Mathf.Clamp(selectionIndex, 0, interactionHit.transform.GetComponent<Interactable>().interactions.Count - 1);
                    for (int i = 0; i < uiController.uiObjects.Count; i++)
                    {
                        uiController.uiObjects[i].GetComponent<InteractionUI>().selected = false;
                    }
                    uiController.uiObjects[selectionIndex].GetComponent<InteractionUI>().selected = true;
                    if(inputManager.interact){
                        inputManager.interact = false;  
                        interactionHit.transform.GetComponent<Interactable>().Interact(selectionIndex);
                        if(!interactionHit.transform.GetComponent<Interactable>()){
                            uiController.ClearInteractions();
                        }
                    }
                }
            }else{                
                uiController.interactionUI.SetActive(false);
            }
        }
    }
}
