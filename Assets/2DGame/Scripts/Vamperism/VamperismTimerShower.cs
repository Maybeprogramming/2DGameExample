using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VamperismTimerShower : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _textCurentSkillState;
    [SerializeField] private Vamperism _vamperism;
    [SerializeField] private string _textActive;
    [SerializeField] private string _textNotActive;

    private void Start()
    {
        _slider.value = _slider.maxValue;
        _textCurentSkillState.text = _textNotActive;
    }

    private void OnEnable()
    {
        _vamperism.Activated += OnActevated;
        _vamperism.Ended += OnEnded;
    }

    private void OnDisable()
    {
        _vamperism.Activated -= OnActevated;
        _vamperism.Ended -= OnEnded;
    }

    private void OnEnded()
    {
        _textTimer.text = SetText(_vamperism.DurationActiveTime);
        _textCurentSkillState.text = _textNotActive;
    }

    private void OnActevated(float durationTime)
    {
        _textCurentSkillState.text = _textActive;
        StartCoroutine(SliderCountdown(durationTime));
    }

    private IEnumerator SliderCountdown(float durationTime)
    {
        float elapsedTime = 0;
        float durationMaxTime = durationTime;
        float currentTime = durationMaxTime;

        while (elapsedTime <= durationTime)
        {
            currentTime -= Time.deltaTime;

            _slider.value = Mathf.Clamp01(currentTime / durationMaxTime);
            _textTimer.text = SetText(currentTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _slider.value = _slider.maxValue;
    }

    private string SetText(float duration)
    {
        string text = string.Format("{0:f1} sec", duration);
        return text;
    }
}