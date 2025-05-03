using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoint;

    public Action<float> Chanched;
    public bool IsAlive => _healthPoint > 0;
    public float Value => _healthPoint;

    public void TakeDamage(float value)
    {
        Set(value);
        Chanched?.Invoke(Value);
    }

    private void Set(float value)
    {
        _healthPoint = _healthPoint - value > 0 ? _healthPoint - value : 0;

        // В другой скрипт
        if (!IsAlive)
        {
            gameObject.TryGetComponent(out Rigidbody2D rigidbody2D);
            gameObject.TryGetComponent(out CapsuleCollider2D capsuleCollider2D);

            rigidbody2D.gravityScale = 0;
            capsuleCollider2D.enabled = false;
        }
    }
}