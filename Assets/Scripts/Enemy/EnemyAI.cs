using UnityEngine;

public class EnemyAI : EnemyMain
{
    [Header("Combat Settings")]
    public float damageRange = 2f;
    public float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void FixedUpdate()
    {
        Attack();
    }

public override void Attack()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= damageRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);

            lastAttackTime = Time.time;
        }
    }
}

