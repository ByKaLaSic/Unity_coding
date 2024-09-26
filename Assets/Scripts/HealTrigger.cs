using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _healingSoundPrefab;
    [SerializeField] private AudioClip _healingClip;

    private const string _healthItemTag = "HealthItem";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_healthItemTag))
        {
            Destroy(other.gameObject);
            GameObject healingSound = Instantiate(_healingSoundPrefab, transform.position, Quaternion.identity);
            Destroy(healingSound, _healingClip.length);
        }
    }
}
