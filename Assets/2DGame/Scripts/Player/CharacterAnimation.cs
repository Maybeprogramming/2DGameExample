using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimation : MonoBehaviour
{
    private const string Attack = "OnAttack";
    private const string HeavyAttack = "OnHeavyAttack";
    private const string Jump = "OnJump";
    private const string Run = "isRun";
    private const string MoveDirection = "MoveDirection";
    private const string IsGround = "isGround";
    private const string TakeDamage = "TakeDamage";
    private const string IsDead = "isDead";
    private const string Dead = "OnDead";

    [SerializeField] private Animator _animator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Health _health;

    private float _moveDirection;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
        //_playerInputController = GetComponent<PlayerInputController>();
        //_groundDetector = GetComponent<GroundDetector>();
        //_health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _playerInputController.Attacked += OnAttack;
        _playerInputController.HeavyAttacked += OnHeavyAttack;
        _playerInputController.Jumped += OnJump;
        _health.Removed += OnTakeDamage;
        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _playerInputController.Attacked -= OnAttack;
        _playerInputController.HeavyAttacked -= OnHeavyAttack;
        _playerInputController.Jumped -= OnJump;
        _health.Removed -= OnTakeDamage;
        _health.Dead -= OnDead;
    }

    private void OnTakeDamage(float damage)
    {
        if (_health.IsAlive)
            _animator.SetTrigger(TakeDamage);
    }

    public void OnAttack()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(Attack);
    }

    public void OnHeavyAttack()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(HeavyAttack);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (_health.IsAlive == false)
            return;

        _animator.SetBool(IsGround, _groundDetector.IsGrounded);
        _animator.SetTrigger(Jump);
    }

    public void OnGround()
    {
        if (_health.IsAlive)
            _animator.SetBool(IsGround, _groundDetector.IsGrounded);
    }

    public void OnRunning(bool isRunning)
    {
        if (_health.IsAlive)
            _animator.SetBool(Run, isRunning);
    }

    public void OnDead()
    {
        _animator.SetTrigger(Dead);
    }

    private void Update()
    {
        Moving();
        OnGround();
    }

    private void Moving()
    {
        _moveDirection = Math.Abs(_playerInputController.Direction.x);
        _animator.SetFloat(MoveDirection, _moveDirection);
    }
}