using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyController : HealthManager
{
    public EnemyAnimator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Attack()
    {
        StartCoroutine(AttackCo(animator.states[Random.Range(0, animator.states.Count)]));
    }
    

    public IEnumerator AttackCo(AnimState state)
    {
        for (int i = 0; i < animator.states.Count; i++)
        {
            if(animator.states[i].value == state.value)
                animator.anim.SetBool(animator.states[i].value, true); 
        }
        yield return new WaitForSeconds(state.length);
        
        for (int i = 0; i < animator.states.Count; i++)
        {
            if(animator.states[i].value == state.value)
                animator.anim.SetBool(animator.states[i].value, false); 
        }
    }
}
