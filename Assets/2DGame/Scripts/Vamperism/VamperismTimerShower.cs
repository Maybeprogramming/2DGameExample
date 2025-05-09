using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VamperismTimerShower : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _timerTextArea;
    [SerializeField] private TextMeshProUGUI _stateTextArea;
    [SerializeField] private TextMeshProUGUI _pressKeyTextArea;
    [SerializeField] private Vamperism _vamperism;
    [SerializeField] private string _textActive;
    [SerializeField] private string _textNotActive;
    [SerializeField] private string _textRecharging;
    [SerializeField] private string _textPressKey;

    private void Awake()
    {
        _slider.value = _slider.maxValue;
        _stateTextArea.text = _textNotActive;
        _pressKeyTextArea.text = _textPressKey;
    }

    private void OnEnable()
    {
        _vamperism.Activated += OnActevated;
        _vamperism.Ended += OnEnded;
        _vamperism.Recharging += OnRecharging;
    }

    private void OnDisable()
    {
        _vamperism.Activated -= OnActevated;
        _vamperism.Ended -= OnEnded;
        _vamperism.Recharging -= OnRecharging;
    }

    private void OnEnded()
    {
        _timerTextArea.text = SetText(_vamperism.DurationActiveTime);
        _stateTextArea.text = _textNotActive;
        _pressKeyTextArea.text = _textPressKey;
    }

    private void OnRecharging(float rechargingTime)
    {
        _stateTextArea.text = _textRecharging;
        StartCoroutine(Countdown(rechargingTime));
    }

    private void OnActevated(float durationTime)
    {
        _stateTextArea.text = _textActive;
        _pressKeyTextArea.text = String.Empty;
        StartCoroutine(Countdown(durationTime));
    }

    private IEnumerator Countdown(float durationTime)
    {
        float elapsedTime = 0f;
        float currentTime = Mathf.Clamp01(_slider.value) * durationTime;
        float multiplier = currentTime <= _slider.maxValue ? -_slider.maxValue : _slider.maxValue;

        while (elapsedTime <= durationTime)
        {
            currentTime -= Time.deltaTime * multiplier;

            _slider.value = Mathf.Clamp01(currentTime / durationTime);
            _timerTextArea.text = SetText(currentTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private string SetText(float duration)
    {
        return string.Format("{0:f1} sec", duration);
    }
}