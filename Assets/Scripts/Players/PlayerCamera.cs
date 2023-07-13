using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public InputManager inputManager;
    public float mouseSensitvityY;
    public float mouseSensitvityX;
    public Transform playerBody;
    public bool disabled;
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
        }
    }
}
