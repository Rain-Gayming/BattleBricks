using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using Firebase.Auth;

public class PlayerController : MonoBehaviour
{
    public UserInfo info;
    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        info = UserManager.instance.userInfo;
        Destroy(UserManager.instance.gameObject);
        view.Owner.NickName = info.displayName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
