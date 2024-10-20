using UnityEngine;


public class Shell : BulletBase
{
    [SerializeField] private float _spreadAngle = 5f;
    [SerializeField] private float _force = 3;

    private Rigidbody _rigidBody;

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

            Rigidbody rigidbody = healthController.gameObject.GetOrAddRigidbody();

            rigidbody.AddForce(_rigidBody.velocity * _force, ForceMode.Impulse);
        }
    }

    public void Run(Vector3 path)
    {
        transform.SetParent(null);
        gameObject.SetActive(true);
        _rigidBody.WakeUp();

        Vector2 randomSpread = Random.insideUnitCircle * _spreadAngle;
        Vector3 shootDirection = Quaternion.Euler(randomSpread.x, randomSpread.y, 0) * path;
        _rigidBody.AddForce(shootDirection, ForceMode.Impulse);

        StartCoroutine(Die());
    }

    public void Sleep()
    {
        _rigidBody.Sleep();
        gameObject.SetActive(false);
    }
}
