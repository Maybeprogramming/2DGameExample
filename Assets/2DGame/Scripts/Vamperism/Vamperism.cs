using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _hitsCount;
    [SerializeField] private float _durationActiveTime;
    [SerializeField] private float _durationRechargeTime;
    [SerializeField] private float _radiusAction;
    [SerializeField] private Transform _vamperismPosition;

    private WaitForSeconds _waitForRechargeTime;

    public Action<float> Activated;
    public Action Ended;
    public Action<float> Recharging;

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

    private void OnDrawGizmos() =>
        Gizmos.DrawWireSphere(_vamperismPosition.position, _radiusAction);

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

        Activated?.Invoke(_durationActiveTime);

        while (elapsedTime < _durationActiveTime + 0.01f)
        {
            bool canDamage = timeBetweenHits * currentHitsCount - elapsedTime <= 0f;

            if (canDamage)
            {
                Debug.Log($"Hit - {currentHitsCount + 1}, {string.Format("Время {0:f4}", timeBetweenHits * currentHitsCount - elapsedTime)}");
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
        Ended?.Invoke();
        IsActive = false;
    }

    private void ApplyDamage()
    {
        Collider2D[] colliders = GetEnemyColliders();        ;
        DamagingNearestEnemy(GetEnemiesHealth(colliders));
    }

    private void DamagingNearestEnemy(Health[] enemiesHealth)
    {
        if (enemiesHealth.Count() > 0)
        {
            Health enemyHealth = enemiesHealth?.OrderBy(healthEnemy =>
                (healthEnemy.transform.position - transform.position).sqrMagnitude).First();

            enemyHealth?.Remove(_damage);
            _playerHealth.Add(_damage);
        }
    }

    private static Health[] GetEnemiesHealth(Collider2D[] colliders) =>
        colliders.Select(collider => collider.GetComponent<Health>()).ToArray();

    private Collider2D[] GetEnemyColliders() =>
        Physics2D.OverlapCircleAll(_vamperismPosition.position, _radiusAction).
            Where(collider => collider.TryGetComponent<Enemy>(out Enemy enemy)).ToArray();
}