using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class UserManager : MonoBehaviour
{
    public FirebaseUser firebaseUserInfo;
    public UserInfo userInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUserInfo()
    {
        userInfo.displayName = firebaseUserInfo.DisplayName;
        userInfo.uID = firebaseUserInfo.UserId;
        userInfo.email = firebaseUserInfo.Email;
    }
}

[System.Serializable]
public class UserInfo
{
    public string uID;
    public string displayName;
    public string email;
}