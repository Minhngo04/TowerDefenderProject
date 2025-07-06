using UnityEngine;
using UnityEditor;

public class TowerPrefabSetup : MonoBehaviour
{
    [Header("Prefab Setup Settings")]
    public float clickAreaMultiplier = 2.0f;
    public bool useFixedClickArea = true;
    
    // Phương thức để setup tất cả tower prefab trong thư mục Prefabs
    [ContextMenu("Setup All Tower Prefabs")]
    public void SetupAllTowerPrefabs()
    {
        #if UNITY_EDITOR
        // Tìm tất cả prefab trong thư mục Prefabs
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Prefabs" });
        
        int setupCount = 0;
        
        foreach (string guid in prefabPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
            if (prefab != null && IsTowerPrefab(prefab))
            {
                if (SetupTowerPrefab(prefab))
                {
                    setupCount++;
                }
            }
        }
        
        Debug.Log($"🔧 Setup {setupCount} tower prefabs");
        AssetDatabase.SaveAssets();
        #else
        Debug.LogWarning("This method only works in Unity Editor");
        #endif
    }
    
    bool IsTowerPrefab(GameObject prefab)
    {
        // Kiểm tra xem prefab có phải là tower không
        return prefab.GetComponent<Tower>() != null || 
               prefab.GetComponent<TowerUpgrade>() != null ||
               prefab.name.ToLower().Contains("tank") ||
               prefab.name.ToLower().Contains("tower");
    }
    
    bool SetupTowerPrefab(GameObject prefab)
    {
        #if UNITY_EDITOR
        bool modified = false;
        
        // Kiểm tra xem đã có TowerClickHandler chưa
        TowerClickHandler existingHandler = prefab.GetComponent<TowerClickHandler>();
        if (existingHandler == null)
        {
            // Thêm TowerClickHandler
            TowerClickHandler clickHandler = prefab.AddComponent<TowerClickHandler>();
            clickHandler.clickAreaMultiplier = clickAreaMultiplier;
            clickHandler.useFixedClickArea = useFixedClickArea;
            
            // Đánh dấu prefab đã được sửa đổi
            EditorUtility.SetDirty(prefab);
            modified = true;
            
            Debug.Log($"🎯 Added TowerClickHandler to prefab: {prefab.name}");
        }
        
        return modified;
        #else
        return false;
        #endif
    }
    
    // Phương thức để setup prefab được chọn
    [ContextMenu("Setup Selected Prefab")]
    public void SetupSelectedPrefab()
    {
        #if UNITY_EDITOR
        if (Selection.activeGameObject != null)
        {
            GameObject selectedObject = Selection.activeGameObject;
            
            // Kiểm tra xem có phải prefab không
            if (PrefabUtility.IsPartOfPrefabAsset(selectedObject))
            {
                SetupTowerPrefab(selectedObject);
            }
            else
            {
                Debug.LogWarning("Selected object is not a prefab asset");
            }
        }
        #else
        Debug.LogWarning("This method only works in Unity Editor");
        #endif
    }
}

#if UNITY_EDITOR
// Editor script để thêm menu item
public class TowerPrefabSetupEditor
{
    [MenuItem("Tools/Tower Defender/Setup All Tower Prefabs")]
    public static void SetupAllTowerPrefabs()
    {
        TowerPrefabSetup setup = new GameObject("TowerPrefabSetup").AddComponent<TowerPrefabSetup>();
        setup.SetupAllTowerPrefabs();
        Object.DestroyImmediate(setup.gameObject);
    }
    
    [MenuItem("Tools/Tower Defender/Setup Selected Prefab")]
    public static void SetupSelectedPrefab()
    {
        if (Selection.activeGameObject != null)
        {
            TowerPrefabSetup setup = new GameObject("TowerPrefabSetup").AddComponent<TowerPrefabSetup>();
            setup.SetupSelectedPrefab();
            Object.DestroyImmediate(setup.gameObject);
        }
        else
        {
            Debug.LogWarning("Please select a prefab first");
        }
    }
}
#endif 