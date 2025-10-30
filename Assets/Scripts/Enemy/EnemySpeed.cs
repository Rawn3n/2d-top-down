using UnityEditor.UI;
using UnityEngine;

public class EnemySpeed : EnemyMain
{
    public Transform target;
    public float viewRange = 20f;
    private float damageRange = 0.3f;

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector2 direction = target.position - transform.position;
        Debug.DrawLine(transform.position, target.position, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, viewRange);

        if (hit.collider != null && hit.collider.transform == target)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.fixedDeltaTime
            );
        }

        Attack();
    }

    public override void Attack()
    {
        //explosion or melee
        if (damageRange >= Vector2.Distance(transform.position, target.position))
        {
            target.GetComponent<Health>()?.TakeDamage(damage);
            Die();
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        target.GetComponent<Health>()?.TakeDamage(damage);
    //    }
    //}
}
