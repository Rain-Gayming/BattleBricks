using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{    
    public Rigidbody rb;
    public float velocity;
    public float timer;
    public float time;
    public float radius;
    public float damage = 45;
    Collider[] inRange;
    void Start()
    {
        time = timer;
        rb.velocity = transform.forward * velocity;  
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if(time < 0){
            inRange = Physics.OverlapSphere(transform.position, radius);
            //TODO: Deal Damage
            for (int i = 0; i < inRange.Length; i++)
            {
                Debug.Log(inRange[i].gameObject.name);
                if(inRange[i].GetComponent<HealthManager>())
                    inRange[i].GetComponent<HealthManager>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
