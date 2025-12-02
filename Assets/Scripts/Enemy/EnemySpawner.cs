using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 3f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public Transform player;

    private void Start()
    {
        if (player == null)
        {
            return;
        }

        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        int index = Random.Range(0, enemyPrefabs.Length);

        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPos = new Vector2(spawnX, spawnY);

        GameObject enemy = Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);

        var pathfindingSetter = enemy.GetComponent<Pathfinding.AIDestinationSetter>();
        if (pathfindingSetter != null)
        {
            pathfindingSetter.target = player;

        }

        var enemySpeed = enemy.GetComponent<EnemySpeed>();
        if (enemySpeed != null)
        {
            enemySpeed.target = player;
        }
    }
}
