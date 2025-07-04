using UnityEngine;

public class TowerSelectionPanel : MonoBehaviour
{
    public GameObject cannonTowerPrefab;
    public GameObject archerTowerPrefab;

    public void SelectCannonTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(cannonTowerPrefab);
        }
    }

    public void SelectArcherTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(archerTowerPrefab);
        }
    }

}
