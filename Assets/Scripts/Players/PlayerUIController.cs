using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Photon.Pun;

public class PlayerUIController : MonoBehaviour
{
    [BoxGroup("References")]
    public PhotonView view;
    [BoxGroup("References")]
    public PlayerMovement playerMovement;
    [BoxGroup("References")]
    public PlayerController playerController;

    
    [BoxGroup("Movement")]
    public TMP_Text positionText;
    [BoxGroup("Movement")]
    public TMP_Text speedText;
    
    [BoxGroup("Account")]
    public TMP_Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        if(!view.IsMine){
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        positionText.text = "X: " + Mathf.Round(transform.position.x).ToString() + " Y: " + Mathf.Round(transform.position.y).ToString() 
            + " Z: " + Mathf.Round(transform.position.z).ToString();

        speedText.text = playerMovement.rb.velocity.ToString();
        nameText.text = playerController.info.displayName;
    }
}
