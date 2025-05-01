using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(CharacterAnimation), typeof(PlayerInputController), typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FlipperX _flipperX;    
    //[SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipperX = GetComponent<FlipperX>();
        //_groundDetector = GetComponent<GroundDetector>();
    }

    private void OnEnable()
    {
        _playerInputController.Attacked += OnAttack;
        _playerInputController.HeavyAttacked += OnHeavyAttack;
        _playerInputController.Jumped += OnJump;
    }

    private void OnJump()
    {
        //if (_groundDetector.IsGrounded)
        //{
        //    _rigidbody.AddForce(Vector2.up * _jumpForce);
        //}
    }

    private void OnHeavyAttack()
    {

    }

    private void OnAttack()
    {

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