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
    
    [BoxGroup("Paused")]
    public GameObject pauseMenu;
    
    [BoxGroup("Movement")]
    public TMP_Text positionText;
    [BoxGroup("Movement")]
    public TMP_Text speedText;
    
    [BoxGroup("Account")]
    public TMP_Text nameText;

    [BoxGroup("Weapons")]
    public GameObject ammoUI;
    [BoxGroup("Weapons")]
    public TMP_Text ammoText;
    
    [BoxGroup("Weapons/Fire Mode")]
    public Image fireModeIcon;
    [BoxGroup("Weapons/Fire Mode")]
    public FireType fireType;
    [BoxGroup("Weapons/Fire Mode")]
    public Sprite fullAutoSprite;
    [BoxGroup("Weapons/Fire Mode")]
    public Sprite semiAutoSprite;
    [BoxGroup("Weapons/Fire Mode")]
    public Sprite burstSprite;
    
    [BoxGroup("Interaction")]
    public GameObject interactionUI;
    [BoxGroup("Interaction")]
    public Transform interactionGrid;
    [BoxGroup("Interaction")]
    public GameObject interactionObject;
    [BoxGroup("Interaction")]
    public List<GameObject> uiObjects;

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

    public void ChangeFireType(FireType newType)
    {
        switch (newType)
        {
            case FireType.semiAuto:
                fireModeIcon.sprite = semiAutoSprite;
            break;
            case FireType.fullAuto:
                fireModeIcon.sprite = fullAutoSprite;
            break;
            case FireType.burst:
                fireModeIcon.sprite = burstSprite;
            break;
        }
    }

    public void AddNewInteraction(Interaction interaction)
    {
        GameObject newInteraction = Instantiate(interactionObject);
        newInteraction.transform.SetParent(interactionGrid);
        newInteraction.transform.localScale = Vector3.one;
        newInteraction.GetComponentInChildren<InteractionUI>().objectName = interaction.interactionName;
        uiObjects.Add(newInteraction);
    }

    public void ClearInteractions()
    {
        for (int i = 0; i < uiObjects.Count; i++)
        {
            Destroy(uiObjects[i]);
        }
        uiObjects.Clear();
    }
}
