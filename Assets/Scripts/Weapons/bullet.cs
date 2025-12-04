using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 5f;

    private float damage;

    public bool isEnemyBullet = false;
    public bool isPlayerBullet = false;

    public LayerMask ignoreLayers;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void Awake()
    {
        Collider2D bulletCollider = GetComponent<Collider2D>();

        foreach (var col in FindObjectsByType<Collider2D>(FindObjectsSortMode.None))
        {
            if (((1 << col.gameObject.layer) & ignoreLayers.value) != 0)
            {
                Physics2D.IgnoreCollision(bulletCollider, col);
            }
        }
    }

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collided with: " + collision.collider.name);

        if (isPlayerBullet && collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyMain>()?.TakeDamage(damage);
        }

        if (isEnemyBullet && collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Health>()?.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
