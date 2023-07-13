using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using Firebase.Auth;

public class PlayerController : MonoBehaviour
{
    [BoxGroup("References")]
    public UserInfo info;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public GameObject camera;

    [BoxGroup("Models")]
    public GameObject handModel;
    [BoxGroup("Models")]
    public GameObject bodyModel;

    // Start is called before the first frame update
    void Start()
    {
        info = UserManager.instance.userInfo;
        Destroy(UserManager.instance.gameObject);
        view.Owner.NickName = info.displayName;
        if(view.IsMine){
            Destroy(bodyModel);
        }else{
            Destroy(camera);    
            Destroy(handModel);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
