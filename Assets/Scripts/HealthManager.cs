using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HealthManager : MonoBehaviour
{
    [BoxGroup("Health")]
    public float maxHealth = 100;
    [BoxGroup("Health")]
    public float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() 
    {
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Heal(float healing)
    {
        currentHealth += healing;
    }

    public virtual void Die()
    {

    }
}
