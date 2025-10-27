using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
public class EnemyPatrol : EnemyMain
{
    public List<Transform> patrolPoints = new List<Transform>();

    private int currentPointIndex = 0;

    private void FixedUpdate()
    {
        if (patrolPoints.Count == 0 || patrolPoints == null)
        {
            Debug.LogWarning("No patrol points assigned.");
            return;
        }

        if (patrolPoints.Count == 0)
        {
            return;
        }

        if (currentPointIndex >= patrolPoints.Count)
        {
                currentPointIndex = 0;
        }

        Transform targetPoint = patrolPoints[currentPointIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.fixedDeltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex += 1;
        }
    }



}
