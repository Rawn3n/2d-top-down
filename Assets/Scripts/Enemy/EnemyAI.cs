using NUnit.Framework.Constraints;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : EnemyMain
{
    public float damageRange = 5f;
    public GameObject bulletPrefab;
    public override void Attack()
    {
        if (damageRange >= Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)) 
        {
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //Rigidbody rb = bullet.GetComponent<Rigidbody>();
            //rb.linearVelocity = transform.forward * 5f; //bulletSpeed;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}