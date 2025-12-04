using UnityEngine;

public abstract class WeaponMelee : WeaponMain
{
    [Header("Melee Settings")]
    public float attackRange = 1.5f;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    public Transform swordVisual;
    protected void DoMeleeHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<EnemyMain>()?.TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

