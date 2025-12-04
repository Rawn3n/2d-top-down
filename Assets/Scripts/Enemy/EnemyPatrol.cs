using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : EnemyMain
{
    [Header("Patrol Settings")]
    public List<Transform> patrolPoints = new List<Transform>();
    private int currentPointIndex = 0;

    [Header("Ranged Attack Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;
    public float bulletSpeed = 8f;
    public float attackRange = 5f;

    private float nextShootTime = 0f;

    [Header("Detection Settings")]
    public Transform target;
    public float viewRange = 6f;
    public LayerMask playerLayer;
    public float chaseSpeedMultiplier = 1.5f;

    private float stoppingDistance = 0.5f;
    private Rigidbody2D rb;

    private enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Dying,
        Dead
    }

    [SerializeField] private State currentState = State.Patrolling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }

        if (speed <= 0) speed = 2f;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                if (LookForPlayer())
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (LookForPlayer())
                {
                    ChasePlayer();
                }
                else
                {
                    currentState = State.Patrolling;
                }
                break;

            case State.Attacking:
                Attack();
                break;

            case State.Dying:
                Die();
                break;

            case State.Dead:
                break;
        }
    }

    private bool LookForPlayer()
    {
        if (target == null) return false;

        Vector2 direction = target.position - transform.position;
        if (direction.magnitude > viewRange) return false;

        Vector2 start = (Vector2)transform.position + direction.normalized * 0.2f;

        Debug.DrawRay(start, direction.normalized * viewRange, Color.cyan);
        Debug.DrawLine(start, target.position, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(start, direction.normalized, viewRange, playerLayer);
        if (hit.collider != null && hit.collider.transform == target)
        {
            return true;
        }

        return false;
    }

    private void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector2 direction = targetPoint.position - transform.position;

        rb.linearVelocity = direction.normalized * speed;

        if (Vector2.Distance(transform.position, targetPoint.position) < stoppingDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        }
    }

    private void ChasePlayer()
    {
        if (target == null) return;

        Vector2 direction = target.position - transform.position;
        rb.linearVelocity = direction.normalized * speed * chaseSpeedMultiplier;

        float dist = Vector2.Distance(transform.position, target.position);
        if (dist < attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            currentState = State.Attacking;
        }
    }

    public override void Attack()
    {

        rb.linearVelocity = Vector2.zero;

        if (Time.time < nextShootTime)
            return;

        if (bulletPrefab == null)
        {
            return;
        }

        if (firePoint == null)
        {
            return;
        }

        if (target == null)
        {
            currentState = State.Patrolling;
            return;
        }

        Vector2 direction = (target.position - firePoint.position).normalized;

        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet bulletScript = b.GetComponent<bullet>();
        if (bulletScript != null)
        {
            bulletScript.isEnemyBullet = true;
            bulletScript.isPlayerBullet = false;
            bulletScript.ignoreLayers = LayerMask.GetMask("Enemy");

            bulletScript.SetDamage(damage);
        }

        Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
        if (brb != null)
        {
            brb.linearVelocity = direction * bulletSpeed;
        }

        nextShootTime = Time.time + shootCooldown;
        currentState = State.Chasing;
    }

    protected override void Die()
    {
        rb.linearVelocity = Vector2.zero;
        currentState = State.Dead;
        Destroy(gameObject, 0f);
        SceneManager.LoadScene("WinScene"); // ændre den her til et andet script senere!!!
    }
}
