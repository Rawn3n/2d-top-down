using UnityEngine;

public class bullet : MonoBehaviour
{
[SerializeField] private float damage = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyMain>()?.TakeDamage(damage);
        }
    }   
}
