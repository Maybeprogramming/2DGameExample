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
    public bool IsActive { get; private set; }

    public float DurationActiveTime => _durationActiveTime;
    public float DurationRachargeTime => _durationRechargeTime;

    private void Start()
    {
        _input = GetComponent<PlayerInputController>();
        _spriteRenderer.enabled = false;
        IsActive = false;
    }

    private void OnEnable()
    {
        _input.VamperismActivated += OnVamperismActevated;
    }

    private void OnDisable()
    {
        _input.VamperismActivated -= OnVamperismActevated;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_vamperismPosition.position, _radiusAction);
    }

    private void OnVamperismActevated()
    {
        if (IsActive == false)
        {
            Debug.Log("Вампиризм ^^");
            IsActive = true;
            StartCoroutine(VamperismActivating(_durationActiveTime));
        }
    }

    private IEnumerator VamperismActivating(float duration)
    {
        float elapsedTime = 0;
        float timeBetweenHit = duration / --_hitsCount;
        int currentHitsCount = 0;
        Collider2D[] colliders;
        Health[] enemiesHealth;

        Activated?.Invoke(duration);

        while (elapsedTime <= duration)
        {
            _spriteRenderer.enabled = true;
            bool canDamage = elapsedTime - currentHitsCount * timeBetweenHit >= 0;

            if (currentHitsCount < _hitsCount && canDamage)
            {
                Debug.Log($"Прошло тиков: {currentHitsCount + 1}");

                colliders = Physics2D.OverlapCircleAll(_vamperismPosition.position, _radiusAction).
                    Where(collider => collider.TryGetComponent<Enemy>(out Enemy enemy) == true).ToArray();

                enemiesHealth = colliders.Select(collider => collider.GetComponent<Health>()).ToArray();

                foreach (Health enemyHealth in enemiesHealth)
                {
                    enemyHealth.Remove(_damage);
                }

                currentHitsCount++;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log($"Прошло тиков: {currentHitsCount + 1}");

        colliders = Physics2D.OverlapCircleAll(_vamperismPosition.position, _radiusAction).
                    Where(collider => collider.TryGetComponent<Enemy>(out Enemy enemy) == true).ToArray();

        enemiesHealth = colliders.Select(collider => collider.GetComponent<Health>()).ToArray();

        foreach (Health enemy in enemiesHealth)
        {
            enemy.Remove(_damage);
        }

        _spriteRenderer.enabled = false;

        Ended?.Invoke();

        StartCoroutine(Countdown(_durationRechargeTime));
    }

    private IEnumerator Countdown(float rechargeDurationTime)
    {
        Activated?.Invoke(rechargeDurationTime);
        yield return new WaitForSeconds(rechargeDurationTime);
        Ended?.Invoke();
        IsActive = false;
    }
}