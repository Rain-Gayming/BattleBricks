using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

public class GunManager : MonoBehaviour
{
    [BoxGroup("References")]
    public Animator anim;
    [BoxGroup("References")]
    public PlayerController playerController;
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public GunItem gunItem;
    [BoxGroup("References")]
    public InputManager inputManager;

    [BoxGroup("Gun")]
    public FireType currentFireType;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponentInParent<PhotonView>();
        if(!view.IsMine){
            Destroy(this);
        }else{
            playerController = GetComponentInParent<PlayerController>();
            anim = GetComponentInParent<Animator>();
            inputManager = GetComponentInParent<InputManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetLayerWeight(gunItem.animLayer, 1);
        switch (currentFireType)
        {
            case FireType.semiAuto:

            break;                
        }
    }

    public IEnumerator BurstShoot()
    {
        Shoot();
        yield return new WaitForSeconds(gunItem.burstRate);
        Shoot();
        yield return new WaitForSeconds(gunItem.burstRate);
        Shoot();
    }

    public void Shoot()
    {

    }
}
