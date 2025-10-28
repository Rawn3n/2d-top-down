using UnityEngine;

public class EnemySpeed : EnemyMain
{
    public Transform target;
    public float viewRange = 20f;

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
    }

    public override void Attack()
    {
        
    }
}
