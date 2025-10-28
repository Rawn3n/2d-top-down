using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{
    public float damage;
    public float health;
    public float speed;


    public abstract void Attack();
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
