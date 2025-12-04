using UnityEngine;
using UnityEngine.UI;

public class CooldownCursor : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Image cooldownImage;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        if (playerCombat.currentWeapon == null)
        {
            cooldownImage.fillAmount = 0f;
            return;
        }

        WeaponMain weapon = playerCombat.currentWeapon;

        float timeBetweenShots = 1f / weapon.attackRate;
        float remainingCooldown = weapon.nextAttackTime - Time.time;

        if (remainingCooldown < 0)
        {
            remainingCooldown = 0;

        }

        float cooldownFraction = remainingCooldown / timeBetweenShots;
        cooldownImage.fillAmount = cooldownFraction;
    }
}
