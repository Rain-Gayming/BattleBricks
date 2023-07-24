using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WeaponRecoil : MonoBehaviour
{

    public bool aiming;
    public GunItem currentGun;

    [BoxGroup("Rotation")]
    public Vector3 currentRotation;

    [BoxGroup("Rotation")]
    public Vector3 targetRotation;

    [BoxGroup("Snapping")]
    public float snappiness;
    [BoxGroup("Snapping")]
    public float returnSpeed;

    public void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if(!aiming)
            targetRotation += new Vector3(currentGun.recoilX, Random.Range(-currentGun.recoilY, currentGun.recoilY), Random.Range(-currentGun.recoilZ, currentGun.recoilZ));
        else
            targetRotation += new Vector3(currentGun.aimRecoilX, Random.Range(-currentGun.aimRecoilY, currentGun.aimRecoilY), Random.Range(-currentGun.aimRecoilZ, currentGun.aimRecoilZ));
    }
}
