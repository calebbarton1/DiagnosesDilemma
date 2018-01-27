
public static class HelperClass
{
    public static bool RandomBool()
    {
        if (UnityEngine.Random.value >= 0.5f)
            return true;
        else
            return false;
    }
}
