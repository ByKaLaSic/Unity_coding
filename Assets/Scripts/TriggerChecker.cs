using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private const string _healthItemTag = "HealthItem";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_healthItemTag))
        {
            Destroy(other.gameObject);
        }
    }
}
