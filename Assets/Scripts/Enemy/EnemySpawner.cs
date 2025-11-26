using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de spawn")]
    public GameObject enemyToSpawn;
    public float timeToSpawn = 2f;

    public Transform minSpawnPoint;
    public Transform maxSpawnPoint;

    private float spawnCounter;
    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("Despawn por frame")]
    public int checkPerFrame = 5;
    private int enemyToCheck = 0;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;
    private bool bossSpawned = false;

    public static int EnemigosMuertos = 0;

    void Start()
    {
        spawnCounter = timeToSpawn;

        Vector3 center = (minSpawnPoint.position + maxSpawnPoint.position) / 2f;
        despawnDistance = Vector3.Distance(minSpawnPoint.position, maxSpawnPoint.position) + 7f;

        currentWave--;
        GoToNextWave(); 
    }

    void Update()
    {
        if (!PlayerHealth.instance.gameObject.activeSelf) return;
        if (currentWave >= waves.Count) return;

        waveCounter -= Time.deltaTime;
        spawnCounter -= Time.deltaTime;

        WaveInfo wave = waves[currentWave];

        
        if (!wave.isBossWave && spawnCounter <= 0)
        {
            spawnCounter = wave.timeBetweenSpawns;

            GameObject newEnemy = Instantiate(
                wave.enemyToSpawn,
                SelectSpawnPosition(),
                Quaternion.identity
            );
            spawnedEnemies.Add(newEnemy);
        }

        if (waveCounter <= 0)
        {
            GoToNextWave();
        }

        
        int checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                GameObject enemy = spawnedEnemies[enemyToCheck];

                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance > despawnDistance)
                    {
                        Destroy(enemy);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                break;
            }
        }

        if (enemyToCheck >= spawnedEnemies.Count)
        {
            enemyToCheck = 0;
        }
    }

    public void GoToNextWave()
    {
        currentWave++;

        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        WaveInfo wave = waves[currentWave];
        waveCounter = wave.waveLength;
        spawnCounter = wave.timeBetweenSpawns;

        
        if (!wave.isBossWave)
        {
            bossSpawned = false;
        }

        
        if (wave.isBossWave && !bossSpawned)
        {
            GameObject boss = Instantiate(
                wave.enemyToSpawn,
                SelectSpawnPosition(),
                Quaternion.identity
            );
            spawnedEnemies.Add(boss);
            bossSpawned = true;
            spawnCounter = float.MaxValue;
            Debug.Log("🧠 Jefe instanciado al entrar en la wave: " + wave.enemyToSpawn.name);
        }
    }

    private Vector2 SelectSpawnPosition() 
    {
        return new Vector2(
            Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x),
            Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y)
        );
    }

    public static void RegistrarMuerte()
    {
        EnemigosMuertos++;
        Debug.Log($"💀 Enemigos derrotados: {EnemigosMuertos}");
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
    public bool isBossWave = false;
}
