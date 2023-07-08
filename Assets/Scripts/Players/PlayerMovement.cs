using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [BoxGroup("References")]
    public InputManager inputManager;
    [BoxGroup("References")]
    public Rigidbody rb;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public Transform lookPoint;

    
    [BoxGroup("Ground Movement")]
    public MoveType currentMoveType;
    [BoxGroup("Ground Movement/Speeds")]
    public float currentSpeed;
    [BoxGroup("Ground Movement/Speeds")]
    public float walkSpeed;
    [BoxGroup("Ground Movement/Speeds")]
    public float sprintSpeed;
    [BoxGroup("Ground Movement/Speeds")]
    public float crouchSpeed;
    [BoxGroup("Ground Movement/Speeds")]
    public float crawlSpeed;
    [BoxGroup("Ground Movement")]
    public float drag;


    
    [BoxGroup("Ground Check")]
    public LayerMask groundMask;
    [BoxGroup("Ground Check")]
    public Transform groundCheckPoint;
    [BoxGroup("Ground Check")]
    public float range;
    [BoxGroup("Ground Check")]
    public bool isGrounded;
    [BoxGroup("Ground Check")]
    public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!view.IsMine){
            Destroy(this);
        }
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
#region Jumping
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, range, groundMask);
        if(isGrounded){
            rb.drag = drag;
            if(inputManager.jumpValue){
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                
                inputManager.jumpValue = false;
            }
        }else{
            rb.drag = 0;
        }
#endregion

#region Change Move Type
        if(inputManager.sprintValue){
            currentMoveType = MoveType.sprinting;
        }else{
            currentMoveType = MoveType.walking;
        }
        if(inputManager.crouchValue){
            if(currentMoveType != MoveType.crouching){
                currentMoveType = MoveType.crouching;
            }else{
                currentMoveType = MoveType.walking;
            }
        }
        if(inputManager.layValue){
            if(currentMoveType != MoveType.crawling){
                currentMoveType = MoveType.crawling;
            }else{
                currentMoveType = MoveType.walking;
            }
        }
#endregion
    
#region Ground Movement

        switch (currentMoveType)
        {
            case MoveType.walking:
                currentSpeed = walkSpeed;
            break;
            
            case MoveType.sprinting:
                currentSpeed = sprintSpeed;
            break;
            
            case MoveType.crawling:
                currentSpeed = crawlSpeed;
            break;
            
            case MoveType.crouching:
                currentSpeed = crouchSpeed;
            break;
        }

        SpeedControl();
#endregion
    
    }

    private void FixedUpdate() 
    {
        Vector3 move = lookPoint.forward * inputManager.walkValue.y + lookPoint.right * inputManager.walkValue.x;

        rb.AddForce(move.normalized * currentSpeed * 10f, ForceMode.Force);
    }

    public void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > currentSpeed){
            Vector3 limitedVel = flatVel.normalized * currentSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}

public enum MoveType
{
    walking,
    sprinting,
    crouching,
    crawling
}