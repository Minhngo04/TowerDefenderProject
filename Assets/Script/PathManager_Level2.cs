using System.Collections.Generic;
using UnityEngine;

public class PathManager_Level2 : MonoBehaviour
{
    public static List<Vector3Int> path1 = new List<Vector3Int>
    {
        new Vector3Int(-11, 4, 0),
        new Vector3Int(-11, 2, 0),
        new Vector3Int(-1, 2, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(12, 0, 0)
    };

    public static List<Vector3Int> path2 = new List<Vector3Int>
    {
        new Vector3Int(-9, -5, 0),
        new Vector3Int(-9, -3, 0),
        new Vector3Int(-2, -3, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(12, 0, 0)
    };
}
