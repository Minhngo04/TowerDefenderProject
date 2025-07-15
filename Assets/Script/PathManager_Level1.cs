using System.Collections.Generic;
using UnityEngine;

public class PathManager_Level1 : MonoBehaviour
{
    public static List<Vector3Int> path1 = new List<Vector3Int>
    {
        new Vector3Int(-12, 1, 0),
        new Vector3Int(-6, 1, 0),
        new Vector3Int(-6, -3, 0),
        new Vector3Int(12, -2, 0)
    };

    public static List<Vector3Int> path2 = new List<Vector3Int>
    {
        new Vector3Int(-12, 1, 0),
        new Vector3Int(-6, 1, 0),
        new Vector3Int(-6, -3, 0),
        new Vector3Int(-1, -3, 0),
        new Vector3Int(-1, -2, 0),
        new Vector3Int(1, -2, 0),
        new Vector3Int(1, 3, 0),
        new Vector3Int(12, 3, 0)
    };
}
