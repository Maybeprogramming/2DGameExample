using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoint;

    public Action<float> Chanched;
    public Action<float> Added;
    public Action<float> Removed;
    public bool IsAlive => _healthPoint > 0;
    public float Value => _healthPoint;

    public void Remove(float value)
    {
        if (value > 0 && IsAlive)
            _healthPoint = _healthPoint - value > 0 ? _healthPoint - value : 0;

        Removed?.Invoke(value);
        Chanched?.Invoke(Value);
        OnDead();
    }

    public void Add(float value)
    {
        if (value > 0 && IsAlive)
            _healthPoint += value;

        Added?.Invoke(Value);
        Chanched?.Invoke(Value);
    }

    private void OnDead()
    {
        if (IsAlive == false)
        {
            gameObject.TryGetComponent(out Rigidbody2D rigidbody2D);
            gameObject.TryGetComponent(out CapsuleCollider2D capsuleCollider2D);

            rigidbody2D.isKinematic = true;
            capsuleCollider2D.enabled = false;
        }
    }
}