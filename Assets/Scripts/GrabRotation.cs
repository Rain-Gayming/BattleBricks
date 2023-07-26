using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotation : MonoBehaviour
{
    public float rotationSpeed;


    public void OnMouseDrag()
    {
        float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.down, xRot);
        transform.Rotate(Vector3.right, yRot);
    }
}
