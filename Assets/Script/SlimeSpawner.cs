using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public int slimeCount = 5;

    void Start()
    {
        StartCoroutine(SpawnSlimes());
    }

    IEnumerator SpawnSlimes()
    {
        Vector3 startPoint = PathManager.path1[0]; // điểm bắt đầu chung

        for (int i = 0; i < slimeCount; i++)
        {
            // Tạo slime
            GameObject slime = Instantiate(slimePrefab, startPoint, Quaternion.identity);

            // Random chọn path
            var path = Random.value > 0.5f ? PathManager.path1 : PathManager.path2;

            // Gán path cho slime
            var move = slime.GetComponent<SlimeMovement>();
            move.SetPath(path);

            // Lệch nhẹ để tránh đè
            slime.transform.position += new Vector3(0, i * 0.1f, 0);

            // Đợi 1.5 giây trước khi tạo con tiếp theo
            yield return new WaitForSeconds(1.5f);
        }
    }
}
