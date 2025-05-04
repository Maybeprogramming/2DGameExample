using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _textHealth;

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
        if (value >  _maxHealth)
            _maxHealth = value;

        _slider.value = value > 0 ? value / _maxHealth : 0;
        _textHealth.text = value > 0 ? value.ToString(): "0";
    }
}