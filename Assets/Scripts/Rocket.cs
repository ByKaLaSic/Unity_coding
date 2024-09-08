using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Rocket : MonoBehaviour
{
    [SerializeField] private float _powerExplosion;
    [SerializeField] private float _scale;

    private Rigidbody _rigidbody;
    private Collider[] _collidedObjects;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collidedObjects = new Collider[128];
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
                if (healthController.CanTakeDamage(healthController.MaxHp))
                {
                    return;
                }

                if (healthController.TryGetComponent(out Rigidbody rigidbody) == false)
                {
                    rigidbody = collision.gameObject.AddComponent<Rigidbody>();
                }

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
    }

    public void Sleep(Vector3 startPoint)
    {
        _rigidbody.Sleep();
        _rigidbody.isKinematic = true;
        transform.position = startPoint;
    }
}
