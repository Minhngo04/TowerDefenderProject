using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnInterval = 2.5f;

    [Header("Gold Reward")]
    public int goldReward = 10;
    public bool useCustomGold = false;
}

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> waves = new List<EnemyWave>();
    public float waveInterval = 5f;
    public int aliveEnemyCount = 0;
    public Tilemap tilemap; // Gán tilemap trong Inspector

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            EnemyWave wave = waves[waveIndex];

            for (int i = 0; i < wave.enemyCount; i++)
            {
                // ✅ Chọn path theo scene
                List<Vector3Int> path;
                if (currentScene == "Level_2")
                {
                    path = Random.value > 0.5f ? PathManager_Level2.path1 : PathManager_Level2.path2;
                }
                else
                {
                    path = Random.value > 0.5f ? PathManager_Level1.path1 : PathManager_Level1.path2;
                }

                // ✅ Lấy vị trí world từ tọa độ tile
                Vector3 worldSpawn = tilemap.GetCellCenterWorld(path[0]);

                GameObject enemy = Instantiate(wave.enemyPrefab, worldSpawn, Quaternion.identity);
                aliveEnemyCount++;

                // ✅ Gán phần thưởng nếu có
                if (wave.useCustomGold)
                {
                    EnemyData enemyData = enemy.GetComponent<EnemyData>();
                    if (enemyData == null)
                    {
                        enemyData = enemy.AddComponent<EnemyData>();
                    }
                    enemyData.goldReward = wave.goldReward;
                }

                // ✅ Gán đường đi
                EnemyMovement move = enemy.GetComponent<EnemyMovement>();
                if (move != null)
                {
                    // Chuyển tất cả path từ tile sang world
                    List<Vector3> worldPath = new List<Vector3>();
                    foreach (var cell in path)
                    {
                        worldPath.Add(tilemap.GetCellCenterWorld(cell));
                    }

                    move.SetPath(worldPath);
                }

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // ✅ Chờ đến khi enemy wave này chết hết
            while (aliveEnemyCount > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waveInterval);
        }

        yield return new WaitForSeconds(2f);

        // ✅ Chuyển màn chơi
        if (currentScene == "Level_2")
        {
            SceneManager.LoadScene("EndMenu");
        }
        else
        {
            SceneManager.LoadScene("Level_2");
        }
    }

    public void NotifyEnemyDeath()
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);
    }
}
