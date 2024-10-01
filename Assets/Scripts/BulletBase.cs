using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;

    public int Damage => _damage;

    protected IEnumerator Die()
    {
        while (_lifeTime >= 0.0f)
        {
            _lifeTime -= 1;
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(gameObject);
    }
}
