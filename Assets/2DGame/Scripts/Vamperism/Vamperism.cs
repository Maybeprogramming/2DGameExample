using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private float _damage;
    [SerializeField] private float _hitsCount;
    [SerializeField] private float _durationActiveTime;
    [SerializeField] private float _durationRechargeTime;
    [SerializeField] private float _radiusAction;
    [SerializeField] private Transform _vamperismPosition;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Action<float> Activated;
    public Action Ended;
    public Action<float> Recharging;
    public bool IsActive { get; private set; }
    public float DurationActiveTime => _durationActiveTime;

    private WaitForSeconds _waitForRechargeTime;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
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
            StartCoroutine(VamperismActivating(_durationActiveTime));
        }
    }

    private IEnumerator VamperismActivating(float durationTime)
    {
        float elapsedTime = 0;
        float timeRanges = _hitsCount - 1;
        float timeBetweenHits = durationTime / timeRanges;
        int currentHitsCount = 0;

        Activated?.Invoke(durationTime);

        while (elapsedTime <= durationTime)
        {
            _spriteRenderer.enabled = true;
            bool canDamage = elapsedTime - timeBetweenHits * currentHitsCount >= 0f;

            if (currentHitsCount < _hitsCount && canDamage)
            {
                ApplyDamage();
                currentHitsCount++;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ApplyDamage();
        _spriteRenderer.enabled = false;
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
        Collider2D[] colliders = GetEnemyColliders();
        Health[] enemiesHealth = GetEnemiesHealth(colliders);
        DamagingNearestEnemy(enemiesHealth);
    }

    private void DamagingNearestEnemy(Health[] enemiesHealth)
    {
        Health enemyHealth = enemiesHealth?.OrderBy(healthEnemy =>
            (healthEnemy.transform.position - transform.position).magnitude).FirstOrDefault();

        enemyHealth?.Remove(_damage);
    }

    private static Health[] GetEnemiesHealth(Collider2D[] colliders) =>
        colliders.Select(collider => collider.GetComponent<Health>()).ToArray();


    private Collider2D[] GetEnemyColliders() =>
        Physics2D.OverlapCircleAll(_vamperismPosition.position, _radiusAction).
            Where(collider => collider.TryGetComponent<Enemy>(out Enemy enemy) == true).ToArray();
}