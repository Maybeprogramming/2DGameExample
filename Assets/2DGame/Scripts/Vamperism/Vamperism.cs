using System;
using System.Collections;
using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private EnemyDetectorByLayerMask _enemyDetectorByLayerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _hitsCount;
    [SerializeField] private float _durationActiveTime;
    [SerializeField] private float _durationRechargeTime;

    private WaitForSeconds _waitForRechargeTime;
    private WaitForSeconds _waitTimeToNextDamage;

    public event Action<float> Activated;
    public event Action Ended;
    public event Action<float> Recharging;
    public event Action RechargingEnded;

    public bool IsActive { get; private set; }
    public float DurationActiveTime => _durationActiveTime;

    private void Awake()
    {
        IsActive = false;
        _waitForRechargeTime = new WaitForSeconds(_durationRechargeTime);

        float timeRanges = _hitsCount - 1;
        float timeBetweenHits = _durationActiveTime / timeRanges;
        _waitTimeToNextDamage = new WaitForSeconds(timeBetweenHits);
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
        int currentHitsCount = 0;
        Activated?.Invoke(_durationActiveTime);

        while (currentHitsCount < _hitsCount)
        {
            ApplyDamage();
            currentHitsCount++;
            yield return _waitTimeToNextDamage;
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
        if (_enemyDetectorByLayerMask.TryGetNearestEnemyHealth(out Health enemyHealth))
        {
            enemyHealth.Remove(_damage, TypeVariableChanging.Periodic);
            _playerHealth.Add(_damage, TypeVariableChanging.Periodic);
        }
    }
}