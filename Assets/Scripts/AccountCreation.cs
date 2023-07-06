using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class AccountCreation : MonoBehaviour
{
    [BoxGroup("Log in")]
    public TMP_InputField logInEmailInput;
    [BoxGroup("Log in")]
    public TMP_InputField logInPasswordInput;
    [BoxGroup("Log in")]
    public TMP_Text logInErrorText;

    
    [BoxGroup("Sign Up")]
    public TMP_InputField signUpUsernameInput;
    [BoxGroup("Sign Up")]
    public TMP_InputField signUpEmailInput;
    [BoxGroup("Sign Up")]
    public TMP_InputField signUpPasswordInput;
    [BoxGroup("Sign Up")]
    public TMP_InputField signUpConfirmPasswordInput;
    [BoxGroup("Sign Up")]
    public TMP_Text signUpErrorText;
    
    public void CreateAccount()
    {
        
    }
}
