using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VamperismTimerUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _textCurentSkillState;
    [SerializeField] private Vamperism _vamperism;
    [SerializeField] private bool _isActive;
    [SerializeField] private string _textActive;
    [SerializeField] private string _textNotActive;

    private float _sliderMaxValue;

    private void Start()
    {
        _sliderMaxValue = 1f;
        _slider.value = _sliderMaxValue;
        _isActive = _vamperism.IsActive;
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
        _isActive = false;
        _textTimer.text = SetText(_vamperism.DurationActiveTime);
        _textCurentSkillState.text = _textNotActive;
    }

    private void OnActevated(float durationTime)
    {
        if (_isActive == false)
        {
            _isActive = true;
            _textCurentSkillState.text = _textActive;
            StartCoroutine(SliderCountdown(durationTime));
        }
    }

    private IEnumerator SliderCountdown(float durationTime)
    {
        float elapsedTime = 0;
        float currentSliderValue;
        float durationMaxTime = durationTime;
        float currentTime = durationMaxTime;

        while (elapsedTime <= durationTime)
        {
            currentTime -= Time.deltaTime;
            currentSliderValue = currentTime / durationMaxTime;
            _slider.value = currentSliderValue;

            _textTimer.text = SetText(currentTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _slider.value = _sliderMaxValue;
    }

    private string SetText(float duration)
    {
        return $"{(int)duration} sec";
    }
}