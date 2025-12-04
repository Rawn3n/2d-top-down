using UnityEngine;

public abstract class WeaponMain : MonoBehaviour
{
    [Header("Base Weapon Stats")]
    public float damage = 10f;
    public float attackRate = 1f;

    public float nextAttackTime = 0f;
    public virtual bool IsFullAuto => false;
    public abstract void Attack();
}

