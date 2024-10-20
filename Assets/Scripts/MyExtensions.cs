using UnityEngine;

public static class MyExtensions
{
    public static Rigidbody GetOrAddRigidbody(this GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Rigidbody rigidbody) == false)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        return rigidbody;
    }
}
