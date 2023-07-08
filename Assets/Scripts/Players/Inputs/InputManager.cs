using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

public class InputManager : MonoBehaviour
{
    [BoxGroup("References")]
    public static InputManager instance;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public PlayerInputs inupts;
#region  movement
    [BoxGroup("Movement")]
    public Vector2 walkValue;
    [BoxGroup("Movement")]
    public bool sprintValue;
    [BoxGroup("Movement")]
    public bool jumpValue;
    [BoxGroup("Movement")]
    public bool layValue;
    [BoxGroup("Movement")]
    public bool crouchValue;
#endregion

#region  combat
    [BoxGroup("Combat")]
    public bool shootValue;
    [BoxGroup("Combat")]
    public bool aimValue;
    [BoxGroup("Combat")]
    public bool nextGunValue;
    [BoxGroup("Combat")]
    public bool previousGunValue;
    [BoxGroup("Combat")]
    public bool primaryValue;
    [BoxGroup("Combat")]
    public bool secondaryValue;
    [BoxGroup("Combat")]
    public bool meleeValue;
    [BoxGroup("Combat")]
    public bool equipmentValue;
    [BoxGroup("Combat")]
    public bool grenadeValue;
#endregion
    [BoxGroup("Camera")]
    public Vector2 lookValue;

    private void Start()
    {
        if(!view.IsMine){
            Destroy(this);
        }else{
            instance = this;
            inupts = new PlayerInputs();
            inupts.Enable();
        }
    }
    void Update()
    {
#region  movement
        walkValue = inupts.Movement.Walk.ReadValue<Vector2>();

        inupts.Movement.Sprint.performed += _ => sprintValue = true;
        inupts.Movement.Sprint.canceled += _ => sprintValue = false;

        inupts.Movement.Jump.performed += _ => jumpValue = true;
        inupts.Movement.Jump.canceled += _ => jumpValue = false;

        inupts.Movement.Crouch.performed += _ => crouchValue = true;
        inupts.Movement.Crouch.canceled += _ => crouchValue = false;

        inupts.Movement.Lay.performed += _ => layValue = true;
        inupts.Movement.Lay.canceled += _ => layValue = false;
#endregion

#region  combat
        inupts.Combat.Shoot.performed += _ => shootValue = true;
        inupts.Combat.Shoot.canceled += _ => shootValue = false;
        inupts.Combat.Aim.performed += _ => aimValue = true;
        inupts.Combat.Aim.canceled += _ => aimValue = false;
        
        inupts.Combat.NextGun.performed += _ => nextGunValue = true;
        inupts.Combat.NextGun.canceled += _ => nextGunValue = false;
        inupts.Combat.PreviousGun.performed += _ => previousGunValue = true;
        inupts.Combat.PreviousGun.canceled += _ => previousGunValue = false;
        
        inupts.Combat.Primary.performed += _ => primaryValue = true;
        inupts.Combat.Primary.canceled += _ => primaryValue = false;
        inupts.Combat.Secondry.performed += _ => secondaryValue = true;
        inupts.Combat.Secondry.canceled += _ => secondaryValue = false;
        inupts.Combat.Melee.performed += _ => meleeValue = true;
        inupts.Combat.Melee.canceled += _ => meleeValue = false;
        inupts.Combat.Equipment.performed += _ => equipmentValue = true;
        inupts.Combat.Equipment.canceled += _ => equipmentValue = false;
        inupts.Combat.Grenade.performed += _ => grenadeValue = true;
        inupts.Combat.Grenade.canceled += _ => grenadeValue = false;
#endregion

#region camera

        lookValue = inupts.Camera.Look.ReadValue<Vector2>();
#endregion
    }
}
