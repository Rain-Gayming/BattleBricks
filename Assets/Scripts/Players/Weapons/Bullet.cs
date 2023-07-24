using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletItem bulletItem;
    public Rigidbody rb;

    void Start()
    {
        rb.velocity = transform.forward * bulletItem.velocity;  
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<HealthManager>()){
            other.GetComponent<HealthManager>().TakeDamage(bulletItem.damage); 
        }
        Destroy(gameObject);
    }
}
