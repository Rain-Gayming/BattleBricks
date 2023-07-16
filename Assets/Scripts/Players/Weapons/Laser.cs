using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserPoint;
    public Transform laser;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(laserPoint.position, laserPoint.forward, out hit, Mathf.Infinity)){
            laser.position = hit.point;
        }
    }
}
