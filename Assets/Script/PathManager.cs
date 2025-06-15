using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static List<Vector3> path1 = new List<Vector3>
    {
        new Vector3(-12, 1, 0),
        new Vector3(-6, 1, 0),
        new Vector3(-6, -3, 0),
        new Vector3(12, -2, 0)
    };

    public static List<Vector3> path2 = new List<Vector3>
    {
        new Vector3(-12, 1, 0),
        new Vector3(-6, 1, 0),
        new Vector3(-6, -3, 0),
        new Vector3(-1, -3, 0),
        new Vector3(-1, -2, 0),
        new Vector3(1, -2, 0),
        new Vector3(1, 3, 0),
        new Vector3(12, 3, 0)
    };
}
