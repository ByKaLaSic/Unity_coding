using UnityEngine;

public sealed class HealTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _healingSoundPrefab;
    [SerializeField] private AudioClip _healingClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CharacterController characterController))
        {
            Destroy(gameObject);
            GameObject healingSound = Instantiate(_healingSoundPrefab, transform.position, Quaternion.identity);
            Destroy(healingSound, _healingClip.length);
        }
    }
}
