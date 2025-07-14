using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
                List<Vector3> path;
                if (currentScene == "Level_2")
                {
                    path = Random.value > 0.5f ? PathManager_Level2.path1 : PathManager_Level2.path2;
                }
                else
                {
                    path = Random.value > 0.5f ? PathManager_Level1.path1 : PathManager_Level1.path2;
                }

                Vector3 spawnPosition = path[0] + new Vector3(0, i * 0.1f, 0);
                GameObject enemy = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
                aliveEnemyCount++;

                // Custom gold
                if (wave.useCustomGold)
                {
                    EnemyData enemyData = enemy.GetComponent<EnemyData>();
                    if (enemyData == null)
                    {
                        enemyData = enemy.AddComponent<EnemyData>();
                    }
                    enemyData.goldReward = wave.goldReward;
                }

                EnemyMovement move = enemy.GetComponent<EnemyMovement>();
                move.SetPath(path);

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            while (aliveEnemyCount > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waveInterval);
        }

        yield return new WaitForSeconds(2f);

        // ✅ Chuyển màn
        if (currentScene == "Level_2")
        {
            SceneManager.LoadScene("EndMenu");
        }
        else
        {
            SceneManager.LoadScene("Level_2");
        }
    }

    // ✅ Gọi khi enemy chết
    public void NotifyEnemyDeath()
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);
    }
}