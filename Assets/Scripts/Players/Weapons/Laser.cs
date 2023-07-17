using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserPoint;
    public Transform laser;
    public AttachmentItem attachmentItem;
    public Material red;
    public Material green;
    public Material blue;
    void Update()
    {
        switch (attachmentItem.laserType)
        {
            case LaserType.red:
                laser.GetComponent<MeshRenderer>().material = red;
            break;    
            case LaserType.green:
                laser.GetComponent<MeshRenderer>().material = green;
            break;    
            case LaserType.blue:
                laser.GetComponent<MeshRenderer>().material = blue;
            break;    
        }
        RaycastHit hit;
        if(Physics.Raycast(laserPoint.position, laserPoint.forward, out hit, Mathf.Infinity)){
            laser.position = hit.point + hit.normal * 0.01f;
            laser.rotation = Quaternion.LookRotation(hit.normal, Vector3.right);
        }
    }
}
