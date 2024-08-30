using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isPhysicalMovement = true;
    [SerializeField] private float _speed = 10f;
    
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _speed);

            if (!_isPhysicalMovement)
                transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        }
    }

    private void FixedUpdate()
    {
        if (_isPhysicalMovement)
            _rigidbody.velocity = _direction * _speed;
    }
}
