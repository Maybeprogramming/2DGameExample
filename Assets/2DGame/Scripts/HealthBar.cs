using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private float _smoothDurationTime;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health.Value;
        _slider.value = _slider.maxValue;
        _textHealth.text = _health.Value.ToString();
    }

    private void OnEnable() =>    
        _health.Changed += OnValueChanching;            

    private void OnDisable() =>    
        _health.Changed -= OnValueChanching;

    private void OnValueChanching(float value) => 
        SetCurrentValue(value);

    private void SetCurrentValue(float value)
    {
        if (value > _maxHealth)
            _maxHealth = value;

        StartCoroutine(HealthSmothing(value));
    }

    private IEnumerator HealthSmothing(float targetValue)
    {
        float targetSliderValue = targetValue / _maxHealth;
        float startSliderValue = _slider.value;
        float timeElapsed = 0f;
        float.TryParse(_textHealth.text, out float startValue);
        float currentValue;

        while (timeElapsed < _smoothDurationTime)
        {
            timeElapsed += Time.deltaTime;
            _slider.value = Mathf.Lerp(startSliderValue, targetSliderValue, timeElapsed / _smoothDurationTime);
            currentValue = Mathf.Lerp(startValue, targetValue, timeElapsed / _smoothDurationTime);
            _textHealth.text = string.Format("{0:f1}", currentValue);

            yield return null;
        }
    }
}