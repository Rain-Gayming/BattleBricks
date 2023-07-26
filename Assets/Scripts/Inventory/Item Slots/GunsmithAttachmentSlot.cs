using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunsmithAttachmentSlot : MonoBehaviour
{
    public AttachmentItem attachmentItem;
    public AttachmentRenderer attachmentRenderer;

    public void Clicked()
    {
        switch (attachmentItem.attachmentType)
        {
            case AttachmentType.scope:
                FindObjectOfType<GunsmithUI>().currentlyEditingGun.scope = attachmentItem;
            break;
            case AttachmentType.barrel:
                FindObjectOfType<GunsmithUI>().currentlyEditingGun.barrel = attachmentItem;
            break;
            case AttachmentType.frontGrip:
                FindObjectOfType<GunsmithUI>().currentlyEditingGun.frontGrip = attachmentItem;
            break;
            case AttachmentType.grip:
                FindObjectOfType<GunsmithUI>().currentlyEditingGun.grip = attachmentItem;
            break;
            case AttachmentType.side:
                FindObjectOfType<GunsmithUI>().currentlyEditingGun.side = attachmentItem;
            break;
        }  
        attachmentRenderer.UpdateAttachment(attachmentItem);
    }

}
