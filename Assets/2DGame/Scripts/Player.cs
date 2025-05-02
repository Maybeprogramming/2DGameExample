using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(CharacterAnimation), typeof(PlayerInputController), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private FlipperX _flipperX;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private Transform _pointDetectorEnemy;
    [SerializeField] private float _radiusDetectorEnemy;
    [SerializeField] private LayerMask _enemyMask;

    private void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _flipperX = GetComponent<FlipperX>();
    }

    private void OnEnable()
    {
        //_playerInputController.Attacked += OnAttack;
        //_playerInputController.HeavyAttacked += OnHeavyAttack;
    }

    #region Дубляж!!!! Убрать! ааа!
    // Дубляж!!!! Убрать! ааа!
    private void HeavyAttack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(_pointDetectorEnemy.position, _radiusDetectorEnemy, _enemyMask);
        
        if (enemy != null)
        {
            enemy.TryGetComponent<Health>(out Health health);
            health.TakeDamage(4);
        }
    }

    private void Attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(_pointDetectorEnemy.position, _radiusDetectorEnemy, _enemyMask);

        if (enemy != null)
        {
            enemy.TryGetComponent<Health>(out Health health);
            health.TakeDamage(2);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_pointDetectorEnemy.position, _radiusDetectorEnemy);
    }
    #endregion

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _flipperX.Flip(_playerInputController.Direction);
        float newPositionX = transform.position.x + _playerInputController.Direction.x * Time.deltaTime * _walkSpeed;
        transform.position = new Vector3(newPositionX, transform.position.y, 0);
    }
}