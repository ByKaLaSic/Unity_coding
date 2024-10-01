using UnityEngine;
using System.Collections;


public sealed class Rocket : BulletBase
{
    private const int COLLISION_SIZE = 128;
    [SerializeField] private float _powerExplosion;
    [SerializeField] private float _scale;

    private Rigidbody _rigidbody;
    private readonly Collider[] _collidedObjects = new Collider[COLLISION_SIZE];

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        float radius = _scale / 2;
        Vector3 center = collision.contacts[0].point;
        int countCollied = Physics.OverlapSphereNonAlloc(center, radius, _collidedObjects);

        for (int i = 0; i < countCollied; i++)
        {
            Collider collidedObject = _collidedObjects[i];

            if (collidedObject.TryGetComponent(out HealthController healthController) == true)
            {
                if (healthController.CanTakeDamage(Damage))
                {
                    return;
                }

                Rigidbody rigidbody = healthController.gameObject.GetOrAddRigidbody();

                rigidbody.AddExplosionForce(_powerExplosion, center, radius);
            }
        }
    }

    public void Run(Vector3 path)
    {
        transform.SetParent(null);
        _rigidbody.WakeUp();
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(path, ForceMode.Impulse);
        StartCoroutine(Die());
    }

    public void Sleep(Vector3 startPoint)
    {
        _rigidbody.Sleep();
        _rigidbody.isKinematic = true;
        transform.position = startPoint;
    }
}
