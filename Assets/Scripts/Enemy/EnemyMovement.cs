using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public AIPath aiPath;

    Vector2 direction;

    private void Update()
    {
        faceVelocity();
    }

    void faceVelocity()
    {
        direction = aiPath.desiredVelocity;

        transform.right = direction;
    }
}


//https://arongranberg.com/astar/download