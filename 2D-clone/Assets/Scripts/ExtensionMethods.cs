using UnityEngine;

public static class ExtensionMethods
{
    public static bool ApproximatelyEquals(this Vector3 v1, Vector3 v2)
    {
        return Mathf.Approximately(v1.x, v2.x) &&
                Mathf.Approximately(v1.y, v2.y) &&
                Mathf.Approximately(v1.z, v2.z);
    }

    public static void Reset(ref this Vector3 v)
    {
        v.x = 0;
        v.y = 0;
        v.z = 0;
    }
}
