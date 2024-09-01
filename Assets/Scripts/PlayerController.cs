using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isPhysicalMovement = true;
    [SerializeField] private float _force = 0.8f;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotateSpeed = 4f;
    [SerializeField] private Animator _animator;

    private bool _isWalking = false;
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        bool shouldWalk = _direction.magnitude >= 0.1f;

        if (_isWalking != shouldWalk)
        {
            _isWalking = shouldWalk;
            _animator.SetBool("isWalking", _isWalking);
        }

        if (_isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);

            if (!_isPhysicalMovement)
                transform.Translate(_direction * _moveSpeed * Time.deltaTime, Space.World); 
        }
    }

    private void FixedUpdate()
    {
        if (_isPhysicalMovement && _isWalking)
            _rigidbody.AddForce(_direction * _force, ForceMode.VelocityChange);
    }
}
