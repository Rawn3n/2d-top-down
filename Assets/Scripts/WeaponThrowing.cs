using UnityEngine;

public abstract class WeaponThrowing : WeaponMain
{
    protected float bulletSpeed = 20.0f;
    protected float fireRate = 0.5f;

    public Transform shootPoint;

    public abstract void Shoot(Transform shootPoint);
}

