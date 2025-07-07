using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnInterval = 2.5f;
}

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> waves = new List<EnemyWave>();
    public float waveInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        Vector3 startPoint = PathManager.path1[0];

        for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            EnemyWave wave = waves[waveIndex];

            for (int i = 0; i < wave.enemyCount; i++)
            {
                GameObject enemy = Instantiate(wave.enemyPrefab, startPoint, Quaternion.identity);

                var path = Random.value > 0.5f ? PathManager.path1 : PathManager.path2;

                var move = enemy.GetComponent<EnemyMovement>();
                move.SetPath(path);

                enemy.transform.position += new Vector3(0, i * 0.1f, 0);

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            yield return new WaitForSeconds(waveInterval);
        }
    }
}
