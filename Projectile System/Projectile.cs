using UnityEngine;

public class Projectile : MonoBehaviour
{
    private enum StartingOrientation
    {
        Forward,
        Back,
        Up,
        Down,
        Right,
        Left,
    }
    
    [SerializeField] private StartingOrientation _startingOrientation;
    [SerializeField] private float _damage = 1.0f;
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _lifetime = 5.0f;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ReceiveDamage(_damage);
            HandleImpact();
        }
    }

    public void Setup(Vector3 startPosition, Vector3 targetPosition)
    {
        _moveDirection = (targetPosition - startPosition).normalized;
        SetOrientation();
        HandleMovement();
        Destroy(gameObject, _lifetime);
    }

    private void SetOrientation()
    {
        switch (_startingOrientation)
        {
            case StartingOrientation.Forward:
                transform.forward = _moveDirection;
                break;
            case StartingOrientation.Back:
                transform.forward = -_moveDirection;
                _moveDirection = -_moveDirection;
                break;
            case StartingOrientation.Up:
                transform.up = _moveDirection;
                break;
            case StartingOrientation.Down:
                transform.up = -_moveDirection;
                _moveDirection = -_moveDirection;
                break;
            case StartingOrientation.Right:
                transform.right = _moveDirection;
                break;
            case StartingOrientation.Left:
                transform.right = -_moveDirection;
                _moveDirection = -_moveDirection;
                break;
        }
    }

    private void HandleMovement() => _rigidbody.AddForce(_moveDirection * _moveSpeed, ForceMode.Impulse);

    private void HandleImpact()
    {
        Destroy(gameObject);
    }
}