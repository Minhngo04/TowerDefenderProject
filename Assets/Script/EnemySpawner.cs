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
        for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            EnemyWave wave = waves[waveIndex];

            for (int i = 0; i < wave.enemyCount; i++)
            {
                // ✅ Random chọn path đúng trước
                List<Vector3> path = Random.value > 0.5f ? PathManager.path1 : PathManager.path2;

                // ✅ Lấy điểm spawn đúng theo path đầu tiên
                Vector3 spawnPosition = path[0] + new Vector3(0, i * 0.1f, 0);

                GameObject enemy = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
                aliveEnemyCount++;

                if (wave.useCustomGold)
                {
                    EnemyData enemyData = enemy.GetComponent<EnemyData>();
                    if (enemyData == null)
                    {
                        enemyData = enemy.AddComponent<EnemyData>();
                    }
                    enemyData.goldReward = wave.goldReward;
                }

                // ✅ Gán path phù hợp đã chọn
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

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level_2")
        {
            SceneManager.LoadScene("EndMenu");
        }
        else
        {
            SceneManager.LoadScene("Level_2");
        }
    }


    // ✅ Gọi phương thức này khi enemy chết
    public void NotifyEnemyDeath()
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);
    }
}
