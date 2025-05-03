using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(CharacterAnimation), typeof(PlayerInputController), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private FlipperX _flipperX;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

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