using System;
using System.Collections;
using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private float _damage;
    [SerializeField] private float _hitsCount;
    [SerializeField] private float _durationActiveTime;
    [SerializeField] private float _durationRechargeTime;

    private WaitForSeconds _waitForRechargeTime;

    public Action<float> Activated;
    public Action Ended;
    public Action<float> Recharging;
    public Action RechargingEnded;

    public bool IsActive { get; private set; }
    public float DurationActiveTime => _durationActiveTime;

    private void Awake()
    {
        IsActive = false;
        _waitForRechargeTime = new WaitForSeconds(_durationRechargeTime);
    }

    private void OnEnable() =>
        _input.VamperismActivated += OnVamperismActevated;

    private void OnDisable() =>
        _input.VamperismActivated -= OnVamperismActevated;

    private void OnVamperismActevated()
    {
        if (IsActive == false)
        {
            IsActive = true;
            StartCoroutine(VamperismActivating());
        }
    }

    private IEnumerator VamperismActivating()
    {
        float elapsedTime = 0;
        float timeRanges = _hitsCount - 1;
        float timeBetweenHits = _durationActiveTime / timeRanges;
        int currentHitsCount = 0;
        float timeDurationOffset = 0.01f;

        Activated?.Invoke(_durationActiveTime);

        while (elapsedTime < _durationActiveTime + timeDurationOffset)
        {
            bool canDamage = timeBetweenHits * currentHitsCount - elapsedTime <= 0f;

            if (canDamage)
            {
                Debug.Log($"Hit - {currentHitsCount + 1}, {string.Format("Калькуляция: {0:f4}", timeBetweenHits * currentHitsCount - elapsedTime)}");
                ApplyDamage();
                currentHitsCount++;

            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Ended?.Invoke();
        StartCoroutine(Countdown(_durationRechargeTime));
    }

    private IEnumerator Countdown(float rechargeDurationTime)
    {
        Recharging?.Invoke(rechargeDurationTime);
        yield return _waitForRechargeTime;
        RechargingEnded?.Invoke();
        IsActive = false;
    }

    private void ApplyDamage()
    {
        if (_enemyDetector.TryGetEnemyHealth(out Health enemyHealth))
        {
            enemyHealth?.Remove(_damage);
            _playerHealth.Add(_damage);
        }
    }
}