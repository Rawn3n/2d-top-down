using UnityEngine;

public abstract class WeaponMain : MonoBehaviour
{
    [Header("Base Weapon Stats")]
    public float damage = 10f;
    public float attackRate = 1f;

    protected float nextAttackTime = 0f;
    public abstract void Attack();
}

