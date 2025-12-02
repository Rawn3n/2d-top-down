using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public WeaponMain currentWeapon;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Attack();
        }
    }
}

