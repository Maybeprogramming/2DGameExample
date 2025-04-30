using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private const string Attack = "OnAttack";
    private const string HeavyAttack = "OnHeavyAttack";
    private const string Jump = "OnJump";
    private const string Run = "isRun";
    private const string Walk = "isWalk";
    private const string Ground = "OnGround";

    [SerializeField] private Animator _animator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerInputController _playerInputController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerInputController.Attacked += OnAttack;
        _playerInputController.HeavyAttacked += OnHeavyAttack;
        _playerInputController.Jumped += OnJump;
    }

    public void OnAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void OnHeavyAttack()
    {
        _animator.SetTrigger(HeavyAttack);
    }

    public void OnJump()
    {
        _animator.SetTrigger(Jump);
    }

    public void OnGround()
    {
        if (_groundDetector.IsGrounded)
        {
            _animator.SetTrigger(Ground);
        }
        else
        {
            _animator.SetTrigger(Jump);
        }
    }

    public void OnWalking(bool isWalking)
    {
        if (_playerInputController.Direction.x != 0f)
        {
            _animator.SetBool(Walk, true);
        }
        else
        {
            _animator.SetBool(Walk, false);
        }
    }

    public void OnRunning(bool isRunning)
    {
        _animator.SetBool(Run, isRunning);
    }

    private void Update()
    {
        OnGround();
    }
}