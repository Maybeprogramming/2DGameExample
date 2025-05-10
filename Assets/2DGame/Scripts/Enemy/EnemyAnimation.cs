using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private const string IsGround = "isGround";
    private const string TakeDamage = "TakeDamage";
    private const string Dead = "OnDead";

    [SerializeField] private Animator _animator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Changed += OnTakeDamage;
    }

    private void OnDisable()
    {
        _health.Changed -= OnTakeDamage;
    }

    private void OnTakeDamage(float damage)
    {
        if (_health.IsAlive)
            _animator.SetTrigger(TakeDamage);
        else
            _animator.SetTrigger(Dead);
    }

    public void OnGround()
    {
        if (_health.IsAlive)
            _animator.SetBool(IsGround, _groundDetector.IsGrounded);
    }

    private void Update()
    {
        OnGround();
    }
}