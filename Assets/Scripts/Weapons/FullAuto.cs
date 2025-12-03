using UnityEngine;
using UnityEngine.InputSystem;

public class FullAuto : WeaponThrowing
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Spread Settings")]
    [SerializeField] private float spreadAngle = 5f;

    public override bool IsFullAuto => true;

    public override void Attack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + (1f / attackRate);

        if (Camera.main == null || shootPoint == null || bulletPrefab == null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;
        Vector2 direction = (mousePos - shootPoint.position).normalized;

        float angleOffset = Random.Range(-spreadAngle, spreadAngle);
        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
        Vector2 spreadDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)).normalized;

        Vector3 spawnPos = shootPoint.position + (Vector3)(spreadDirection * 0.5f);
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, currentAngle - 90f));

        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = spreadDirection * bulletSpeed;

        bullet bulletScript = bulletObj.GetComponent<bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
            bulletScript.isPlayerBullet = true;
        }
    }
}
