using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Sten : WeaponThrowing
{
    public GameObject bulletPrefab;
    private bool isFired = false;




    private void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) //(Input.GetKey(KeyCode.Space)) //(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(shootPoint);
        }
    }

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
