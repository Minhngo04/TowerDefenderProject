using UnityEngine;

public class CampInfo : MonoBehaviour
{
    public string triggerName = "Tower1"; // Mặc định là Tower1, bạn có thể đổi theo camp
    public GameObject archerPrefab; // Gán prefab Archer ở đây
    public Transform spawnPoint;    // Nơi archer xuất hiện (trên nóc tower)

    private bool isBuilt = false;

    // Gọi từ animation event hoặc từ logic kết thúc xây dựng
    public void OnTowerBuilt()
    {
        if (isBuilt) return;

        isBuilt = true;
        SpawnArcher();
    }

    void SpawnArcher()
    {
        // Lấy vị trí gốc từ camp và cộng thêm offset
        Vector3 spawnPos = transform.position + new Vector3(-0.006f, 0.112f, 0f);
        spawnPos.z = 0; // đảm bảo đúng lớp hiển thị camera

        GameObject archer = Instantiate(archerPrefab, spawnPos, Quaternion.identity, transform);

        // Gán scale đúng như bạn đã đo
        archer.transform.localScale = new Vector3(0.814f, 0.784f, 1f);

        Debug.Log($"✅ Archer spawned at {spawnPos} with scale {archer.transform.localScale}");
    }


}
