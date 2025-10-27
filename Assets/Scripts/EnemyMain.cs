using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{
    public int damage;
    public int health;
    public float speed;


    public abstract void Attack();
    protected void TakeDamage(int damageAmount)
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
