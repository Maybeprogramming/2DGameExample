using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public Action DamageTaking;
    public bool IsAlive => _health > 0;

    public void TakeDamage(float value)
    {
        Set(value);
        DamageTaking?.Invoke();
    }

    private void Set(float value)
    {
        _health = _health - value > 0 ? _health - value : 0;
    }

    //For Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}