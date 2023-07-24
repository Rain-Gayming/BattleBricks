using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimator : MonoBehaviour
{
    public TwoBoneIKConstraint leftArm;
    public TwoBoneIKConstraint rightArm;

    public Transform leftArmPoint;
    public Transform rightArmPoint;

    public void ChangeLeftPoint(Transform newPoint)
    {
        leftArm.data.target = newPoint;
    }
    public void ChangeRightPoint(Transform newPoint)
    {
        rightArm.data.target = newPoint;
    }
}
