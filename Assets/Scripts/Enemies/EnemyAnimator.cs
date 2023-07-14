using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public List<AnimState> states;
    public Animator anim;
    
}
[System.Serializable]
public class AnimState
{
    public string value;
    public float length;
}