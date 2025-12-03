 using UnityEngine;

public abstract class WeaponThrowing : WeaponMain
{
    [Header("Throwing Weapon Settings")]
    [SerializeField] protected float bulletSpeed = 20f;

    public Transform shootPoint;
}

