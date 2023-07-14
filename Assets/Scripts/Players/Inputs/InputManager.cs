using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.IO;
using UnityEngine.InputSystem;
using TMPro;
using ES3Internal;
using ES3Types;

public class InputManager : MonoBehaviour
{
    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;

    static KeybindData myKeybindData = new KeybindData();
    public KeybindData shownKeybindData;
    public static string saveFile;
    public bool ignorePhoton;
    [BoxGroup("References")]
    public static InputManager instance;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public static PlayerInputs inputs;
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
    [BoxGroup("Combat")]
    public bool changeFireModeValue;
    [BoxGroup("Combat")]
    public bool reloadValue;
#endregion
    [BoxGroup("Camera")]
    public Vector2 lookValue;

    [BoxGroup("UI Manager")]
    public bool pauseValue;

    private void OnEnable() {
        saveFile = Application.persistentDataPath + "/keybindings.json";
        inputs = new PlayerInputs();
        inputs.Enable();
    }

    private void Start()
    {
        if(view){

            if(!view.IsMine){
                Destroy(this);
            }else{
                instance = this;
                inputs = new PlayerInputs();
                inputs.Enable();
            }
        }
    }
    void Update()
    {
        shownKeybindData = myKeybindData;
        if( view != null && ignorePhoton){
            Debug.Log("View is null or youre ignoring photn");
            return;
        }
#region  movement
        walkValue = inputs.Movement.Walk.ReadValue<Vector2>();

        inputs.Movement.Sprint.performed += _ => sprintValue = true;
        inputs.Movement.Sprint.canceled += _ => sprintValue = false;

        inputs.Movement.Jump.performed += _ => jumpValue = true;
        inputs.Movement.Jump.canceled += _ => jumpValue = false;

        inputs.Movement.Crouch.performed += _ => crouchValue = true;
        inputs.Movement.Crouch.canceled += _ => crouchValue = false;

        inputs.Movement.Lay.performed += _ => layValue = true;
        inputs.Movement.Lay.canceled += _ => layValue = false;
#endregion

#region  combat
        inputs.Combat.Shoot.performed += _ => shootValue = true;
        inputs.Combat.Shoot.canceled += _ => shootValue = false;
        inputs.Combat.Aim.performed += _ => aimValue = true;
        inputs.Combat.Aim.canceled += _ => aimValue = false;
        
        inputs.Combat.NextGun.performed += _ => nextGunValue = true;
        inputs.Combat.NextGun.canceled += _ => nextGunValue = false;
        inputs.Combat.PreviousGun.performed += _ => previousGunValue = true;
        inputs.Combat.PreviousGun.canceled += _ => previousGunValue = false;
        
        inputs.Combat.Primary.performed += _ => primaryValue = true;
        inputs.Combat.Primary.canceled += _ => primaryValue = false;
        inputs.Combat.Secondry.performed += _ => secondaryValue = true;
        inputs.Combat.Secondry.canceled += _ => secondaryValue = false;
        inputs.Combat.Melee.performed += _ => meleeValue = true;
        inputs.Combat.Melee.canceled += _ => meleeValue = false;
        inputs.Combat.Equipment.performed += _ => equipmentValue = true;
        inputs.Combat.Equipment.canceled += _ => equipmentValue = false;
        inputs.Combat.Grenade.performed += _ => grenadeValue = true;
        inputs.Combat.Grenade.canceled += _ => grenadeValue = false;
        inputs.Combat.ChangeFireMode.performed += _ => changeFireModeValue = true;
        inputs.Combat.ChangeFireMode.canceled += _ => changeFireModeValue = false;
        inputs.Combat.Reload.performed += _ => reloadValue = true;
        inputs.Combat.Reload.canceled += _ => reloadValue = false;
#endregion

#region camera

        lookValue = inputs.Camera.Look.ReadValue<Vector2>();
#endregion

#region UI

        inputs.UI.Pause.performed += _ => pauseValue = true;
        inputs.UI.Pause.canceled += _ => pauseValue = false;
#endregion
    }
    

    public static void StartRebind(string actionName, int bindingIndex, TMP_Text statusText)
    {
        
        InputAction action = inputs.asset.FindAction(actionName);

        if(action == null || action.bindings.Count <= bindingIndex){
            Debug.Log("Couldn't find action or binding");
            return;
        }
        
        if(action.bindings[bindingIndex].isComposite){
            var firstPartIndex = bindingIndex + 1;
            if(firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite){
                DoRebind(action, bindingIndex,statusText,true);
            }

        }else{
            DoRebind(action, bindingIndex, statusText, false);
        }
    }

    public static void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText, bool allCompositeParts)
    {
        if(actionToRebind == null || bindingIndex < 0){
            return;
        }

        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if(allCompositeParts){
                var nextBindingIndex = bindingIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite){
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts);
                }
            }

            rebindComplete?.Invoke();

            SaveBindingOverride(actionToRebind);
        });
        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            rebindCanceled?.Invoke();
        });

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start();
    }

    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if(inputs == null){
            inputs = new PlayerInputs();
            inputs.Enable();
        }

        InputAction action = inputs.asset.FindAction(actionName);

        return action.GetBindingDisplayString(bindingIndex);
    }

    public static void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = inputs.asset.FindAction(actionName);

        if(action == null || action.bindings.Count <= bindingIndex){
            Debug.Log("Coudnt find action or binding");
            return;
        }

        if(action.bindings[bindingIndex].isComposite){
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
            {
                action.RemoveBindingOverride(i);
                SaveBindingOverride(inputs.FindAction(actionName));
            }
        }else
        {
            action.RemoveBindingOverride(bindingIndex);
                SaveBindingOverride(inputs.FindAction(actionName));
        }
    }

    public static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            //replace with something like json saving
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);

            string jsonString = inputs.SaveBindingOverridesAsJson();
            //jsonString = JsonUtility.ToJson(jsonString, true);
            myKeybindData.action = action.name;
            myKeybindData.keybind = inputs.FindAction(action.name).bindings[0].overridePath;
            ES3.Save("MyKeybinds " + action.name, myKeybindData, "Keybinds.json");
            
            //Debug.Log(jsonString);
            
            //File.WriteAllText(saveFile, jsonString);
            //JsonUtility.ToJson(jsonString, true);
            
        }
    }

    public static void LoadBindingOverride(InputAction action)
    {
        if(inputs == null){
            inputs = new PlayerInputs();
        }
        if(ES3.KeyExists("MyKeybinds " + action.name)){
            myKeybindData = ES3.Load<KeybindData>("MyKeybinds "  + action.name);

            InputBinding inputBinding = action.bindings[0];
            inputBinding.overridePath = myKeybindData.keybind;
            action.ApplyBindingOverride(0, inputBinding);

            InputAction newAction = inputs.asset.FindAction(action.name);

            newAction.ApplyBindingOverride(inputBinding.overridePath);

            
        }else if(action.bindings[0].overridePath == null){
            inputs = new PlayerInputs();
            inputs.Enable();
        }

            //inputs = ES3.Load<PlayerInputs>("MyKeybinds", saveFile);
        //inputs.LoadBindingOverridesFromJson(saveFile, true);
    }
}

[System.Serializable]
public class KeybindData
{
    //public PlayerInputs inputAction;
    public string action;
    public string keybind;
}