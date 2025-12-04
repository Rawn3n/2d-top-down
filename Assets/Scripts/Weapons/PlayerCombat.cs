using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<WeaponMain> allWeapons;
    public List<WeaponMain> unlockedWeapons;
    public WeaponMain currentWeapon;

    private int currentWeaponIndex = 0;

    void Start()
    {
        if (allWeapons.Count > 0)
        {
            unlockedWeapons.Add(allWeapons[0]);
            currentWeapon = unlockedWeapons[0];
        }
    }

    void Update()
    {
        HandleAttack();
        HandleWeaponSwitch();
    }

    void HandleAttack()
    {
        if (currentWeapon == null) return;

        if (currentWeapon.IsFullAuto)
        {
            if (Input.GetMouseButton(0))
            {
                currentWeapon.Attack();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentWeapon.Attack();
            }
        }
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0) currentWeaponIndex = unlockedWeapons.Count - 1;
            currentWeapon = unlockedWeapons[currentWeaponIndex];
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeaponIndex++;
            if (currentWeaponIndex >= unlockedWeapons.Count) currentWeaponIndex = 0;
            currentWeapon = unlockedWeapons[currentWeaponIndex];
        }

    }

    public void UnlockWeapon(WeaponMain newWeapon)
    {
        if (!unlockedWeapons.Contains(newWeapon))
        {
            unlockedWeapons.Add(newWeapon);
            currentWeapon = newWeapon;
            currentWeaponIndex = unlockedWeapons.Count - 1;
        }
    }

}
