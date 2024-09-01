using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthItem"))
        {
            Destroy(other.gameObject);
        }
    }
}
