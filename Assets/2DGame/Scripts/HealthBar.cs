using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _health.Value;
        SetCurrentValue(_maxHealth);
        _health.Chanched += OnValueChanching;
    }

    private void OnValueChanching(float value)
    {
        SetCurrentValue(value);
    }

    private void SetCurrentValue(float value)
    {
        _slider.value = value > 0 ? value / _maxHealth : 0;
    }
}