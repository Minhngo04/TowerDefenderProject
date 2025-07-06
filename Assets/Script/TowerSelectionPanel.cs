using UnityEngine;

public class TowerSelectionPanel : MonoBehaviour
{
    public GameObject cannonTowerPrefab;
    public GameObject archerTowerPrefab;
    public GameObject wizardTowerPrefab;
    public GameObject iceTowerPrefab;      // Ice Tower
    public GameObject fireTowerPrefab;     // Fire Tower
    public GameObject lightningTowerPrefab; // Lightning Tower

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

    public void SelectWizardTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(wizardTowerPrefab);
        }
    }

    public void SelectIceTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(iceTowerPrefab);
        }
    }

    public void SelectFireTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(fireTowerPrefab);
        }
    }

    public void SelectLightningTower()
    {
        if (CampInfo.selectedCamp != null)
        {
            CampInfo.selectedCamp.BuildTower(lightningTowerPrefab);
        }
    }
}
