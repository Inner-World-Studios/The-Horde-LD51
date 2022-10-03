using UnityEngine;

public static class Utilities
{
    public static bool IsPrefab(this GameObject a_Object)
    {
        return a_Object.scene.rootCount == 0;
    }
}

