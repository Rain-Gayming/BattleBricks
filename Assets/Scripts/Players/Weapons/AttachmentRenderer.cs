using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentRenderer : MonoBehaviour
{
    public InventoryItem gunItem;
    public GameObject gun;
    public AttachmentPoint[] attachmentPoints;

    private void Start()
    {
        StartCoroutine(GunCo());
    }

    public void UpdateAttachment(AttachmentItem attachment)
    {
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            if(attachmentPoints[i].attachment.attachmentType == attachment.attachmentType){
                if(attachmentPoints[i].attachment == attachment){
                    attachmentPoints[i].attachmentObject.SetActive(true);
                }else{
                    attachmentPoints[i].attachmentObject.SetActive(false);
                }    
            }
        }
    }
    public IEnumerator GunCo()
    {
        gun.SetActive(true);
        yield return new WaitForSeconds(0.0005f);
        attachmentPoints = GetComponentsInChildren<AttachmentPoint>(); 
        gun.SetActive(false);
    }
}
