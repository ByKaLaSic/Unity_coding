using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : BulletBase
{
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime = 7.0f;

    public bool IsActive
    {
        get
        {
            return _isActive;
        }
    }

    private Rigidbody _rigidBody;
    private bool _isActive;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.collider.TryGetComponent(out HealthController healthController) == true)
        {
            if (healthController.CanTakeDamage(Damage))
            {
                return;
            }

            if (collision.collider.TryGetComponent(out Rigidbody rigidbody) == false)
            {
                rigidbody = collision.gameObject.AddComponent<Rigidbody>();
            }

            rigidbody.AddForce(_rigidBody.velocity * _force, ForceMode.Impulse);
        }
    }

    public void Run(Vector3 path, Vector3 startPosition)
    {
        transform.position = startPosition;
        transform.SetParent(null);
        gameObject.SetActive(true);
        _rigidBody.WakeUp();
        _rigidBody.AddForce(path, ForceMode.Impulse);
        _isActive = true;
        StartCoroutine(Die());
    }

    public void Sleep()
    {
        _rigidBody.Sleep();
        gameObject.SetActive(false);
        _isActive = false;
    }

    private IEnumerator Die()
    {
        while (_lifeTime >= 0.0f)
        {
            _lifeTime -= 1;
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(gameObject);
    }
}
