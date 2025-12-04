using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 3f;
    public Transform player;
    public PlayerCombat playerCombat;

    [Header("Spawn Area")]
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    [Header("Spawn Safety")]
    public LayerMask wallLayer;
    public float spawnCheckRadius = 0.4f;
    public int maxSpawnAttempts = 20;
    public float minSpawnDistanceFromPlayer = 2f;

    [Header("Debug")]
    public bool showDebugGizmos = true;
    public bool showLiveSpawnTests = false;
    private Vector2 lastTestedSpawnPos;

    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 3f;
    private int currentWave = 1;
    private bool isSpawningWave = false;




    private void Start()
    {
        if (player == null)
        {
            return;
        }

        StartCoroutine(SpawnWaveLoop());
    }

    IEnumerator SpawnWaveLoop()
    {
        while (true)
        {
            if (!isSpawningWave)
            {
                yield return StartCoroutine(SpawnWave());
                CheckWaveUnlocks();
                currentWave++;
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }


    IEnumerator SpawnWave()
    {
        isSpawningWave = true;

        int enemiesThisWave = enemiesPerWave + (currentWave - 1) * 2;

        for (int i = 0; i < enemiesThisWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        isSpawningWave = false;
        Debug.Log("Wave " + currentWave + " | Enemies: " + enemiesThisWave);
    }

    void CheckWaveUnlocks()
    {
        if (playerCombat == null) return;

        if (currentWave == 5 && playerCombat.allWeapons.Count > 1)
        {
            playerCombat.UnlockWeapon(playerCombat.allWeapons[1]);
        }
        else if (currentWave == 10 && playerCombat.allWeapons.Count > 2)
        {
            playerCombat.UnlockWeapon(playerCombat.allWeapons[2]);
        }
    }


    void SpawnEnemy()
    {
        if (player == null || enemyPrefabs.Length == 0) return;

        Vector2 spawnPos = Vector2.zero;
        bool validPosition = false;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPos = new Vector2(spawnX, spawnY);

            lastTestedSpawnPos = spawnPos;

            Collider2D hit = Physics2D.OverlapCircle(spawnPos, spawnCheckRadius, wallLayer);
            if (hit != null)
                continue;

            float distanceToPlayer = Vector2.Distance(spawnPos, player.position);
            if (distanceToPlayer < minSpawnDistanceFromPlayer)
                continue;

            validPosition = true;
            break;
        }

        if (!validPosition) return;

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);

        var pathfindingSetter = enemy.GetComponent<Pathfinding.AIDestinationSetter>();
        if (pathfindingSetter != null)
            pathfindingSetter.target = player;

        var enemySpeed = enemy.GetComponent<EnemySpeed>();
        if (enemySpeed != null)
            enemySpeed.target = player;
    }



    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Gizmos.color = Color.red;

        Vector3 center = new Vector3(
            (spawnAreaMin.x + spawnAreaMax.x) / 2f,
            (spawnAreaMin.y + spawnAreaMax.y) / 2f,
            0f
        );

        Vector3 size = new Vector3(
            Mathf.Abs(spawnAreaMax.x - spawnAreaMin.x),
            Mathf.Abs(spawnAreaMax.y - spawnAreaMin.y),
            0.1f
        );

        Gizmos.DrawWireCube(center, size);

        if (player != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(player.position, minSpawnDistanceFromPlayer);
        }

        if (showLiveSpawnTests)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(lastTestedSpawnPos, spawnCheckRadius);
        }
    }

}

