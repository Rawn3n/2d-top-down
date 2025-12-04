using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Sword : WeaponMelee
{
    public void Start()
    {
        swordVisual.gameObject.SetActive(false);
    }
    IEnumerator SwordSwingEffect()
    {
        swordVisual.gameObject.SetActive(true);

        float duration = 0.15f;
        float t = 0f;

        Quaternion startRot = Quaternion.Euler(0, 0, -80);
        Quaternion endRot = Quaternion.Euler(0, 0, 80);

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;
            swordVisual.localRotation = Quaternion.Slerp(startRot, endRot, lerp);
            yield return null;
        }

        swordVisual.localRotation = Quaternion.identity;
        swordVisual.gameObject.SetActive(false);
    }
    public override void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            DoMeleeHit();
            StartCoroutine(SwordSwingEffect());
        }
    }
}

