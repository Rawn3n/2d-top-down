using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 5f;
    [SerializeField] private float damage = 10f;
    public LayerMask playerLayer;

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);

        Collider2D bulletCollider = GetComponent<Collider2D>();
        foreach (var playerCollider in FindObjectsByType<Collider2D>(FindObjectsSortMode.None))
        {
            if (((1 << playerCollider.gameObject.layer) & playerLayer.value) != 0)
            {
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyMain>()?.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}