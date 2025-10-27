using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void HealHealth(int healAmount)
    { 
        currentHealth += healAmount;
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            //
        }
    }
}
