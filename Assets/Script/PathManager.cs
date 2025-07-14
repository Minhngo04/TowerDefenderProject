using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static List<Vector3> path1 = new List<Vector3>
    {
        new Vector3(-11, 4, 0),
        new Vector3(-11, 2, 0),
        new Vector3(-1, 2, 0),
        new Vector3(-1, 0, 0),
        new Vector3(12, 0, 0)
    };

    public static List<Vector3> path2 = new List<Vector3>
    {
        new Vector3(-9, -5, 0),
        new Vector3(-9, -3, 0),
        new Vector3(-2, - 3, 0),
        new Vector3(-1, 0, 0),
        new Vector3(12, 0, 0)
    };
}
