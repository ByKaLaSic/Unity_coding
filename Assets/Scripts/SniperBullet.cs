using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SniperBullet : MonoBehaviour
{
    [SerializeField] private float _force = 6;
    [SerializeField] private float _lifeTime = 8;

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
            if (healthController.CanTakeDamage(healthController.MaxHp))
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
