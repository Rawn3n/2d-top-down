using UnityEngine;
using UnityEngine.InputSystem;

public class Sten : WeaponThrowing
{
    public GameObject bulletPrefab;
    private bool isFired = false;
    [SerializeField] private float fireRate = 1.5f;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)//(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot(shootPoint);
        }
    }

    public override void Shoot(Transform shootPoint)
    {
        if (isFired) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f; 

        Vector2 direction = (mousePos - shootPoint.position).normalized;

        Vector3 spawnPos = shootPoint.position + (Vector3)(direction * 0.5f);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        isFired = true;
        Invoke(nameof(ResetFire), fireRate);
    }



    private void ResetFire()
    {
        isFired = false;
    }
}
