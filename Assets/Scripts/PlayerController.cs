using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isPhysicalMovement = true;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotateSpeed = 4f;
    [SerializeField] private Animator _animator;

    private const float _movementThreshold = 0.1f;
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

        bool shouldWalk = _direction.magnitude >= _movementThreshold;

        if (_isWalking != shouldWalk)
        {
            _isWalking = shouldWalk;
            _animator.SetBool("isWalking", _isWalking);
        }

        if (_isWalking == true)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);

            if (_isPhysicalMovement == false)
                transform.Translate(_direction * _moveSpeed * Time.deltaTime, Space.World); 
        }
    }

    private void FixedUpdate()
    {
        if (_isPhysicalMovement == true && _isWalking == true)
            _rigidbody.velocity = _direction * _moveSpeed;
    }
}
