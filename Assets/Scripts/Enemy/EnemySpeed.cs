using UnityEditor.UI;
using UnityEngine;

public class EnemySpeed : EnemyMain
{
    public Transform target;
    public float viewRange = 20f;
    private float damageRange = 1f;
    private float offset = 0.7f;

    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 direction = target.position - transform.position;
        Vector2 rayOrigin = (Vector2)transform.position + direction.normalized * offset;

        Debug.DrawLine(rayOrigin, target.position, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction.normalized, viewRange);

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
