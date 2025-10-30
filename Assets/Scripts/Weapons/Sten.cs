using UnityEngine;

public class Sten : WeaponThrowing
{
    public GameObject bulletPrefab;
    private bool isFired = false;

    public override void Shoot (Transform shootPoint)
    {
        if (isFired)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootPoint.forward * bulletSpeed;
        isFired = true;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        collision.collider.GetComponent<EnemyMain>()?.TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}

}
