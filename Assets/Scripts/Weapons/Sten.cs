using UnityEngine;
using UnityEngine.InputSystem;

public class Sten : WeaponThrowing
{
    [Header("Sten Settings")]
    [SerializeField] private GameObject bulletPrefab;

    //private void Update()
    //{
    //    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        Attack();
    //    }
    //}

    public override void Attack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + (1f / attackRate);

        if (Camera.main == null || shootPoint == null || bulletPrefab == null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector2 direction = (mousePos - shootPoint.position).normalized;

        Vector3 spawnPos = shootPoint.position + (Vector3)(direction * 0.5f);
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        bullet bulletScript = bulletObj.GetComponent<bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
            bulletScript.isPlayerBullet = true;
        }
    }
}
