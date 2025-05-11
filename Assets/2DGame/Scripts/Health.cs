using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoint;

    public event Action<float, TypeVariableChanging> Changed;
    public event Action<float> Added;
    public event Action<float> Removed;
    public event Action Dead;
    public bool IsAlive => _healthPoint > 0;
    public float Value => _healthPoint;

    public void Remove(float value, TypeVariableChanging type = TypeVariableChanging.Instant)
    {
        if (value > 0 && IsAlive)
        {
            _healthPoint = Mathf.Clamp(_healthPoint - value, 0, _healthPoint);

            Removed?.Invoke(value);
            Changed?.Invoke(Value, type);
        }

        OnDead();
    }

    public void Add(float value, TypeVariableChanging type = TypeVariableChanging.Instant)
    {
        if (value > 0 && IsAlive)
            _healthPoint += value;

        Added?.Invoke(Value);
        Changed?.Invoke(Value, type);
    }

    private void OnDead()
    {
        if (IsAlive == false)
        {
            gameObject.TryGetComponent(out Rigidbody2D rigidbody2D);
            gameObject.TryGetComponent(out CapsuleCollider2D capsuleCollider2D);

            rigidbody2D.simulated = false;
            rigidbody2D.isKinematic = true;
            capsuleCollider2D.enabled = false;
            Dead?.Invoke();
        }
    }
}