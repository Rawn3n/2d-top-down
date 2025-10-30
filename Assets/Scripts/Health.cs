using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth = 100;
    private float currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void HealHealth(float healAmount)
    { 
        currentHealth += healAmount;
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }

    void Death()
    {
        Debug.Log("Dead");
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            // load scene death screen
            Death();
            currentHealth = 100;
        }
    }
}
