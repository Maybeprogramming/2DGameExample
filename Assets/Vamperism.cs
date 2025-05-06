using System;
using System.Collections;
using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private float _damage;
    [SerializeField] private float _durationActiveTime;
    [SerializeField] private float _durationRechargeTime;
    [SerializeField] private float _radiusAction;
    [SerializeField] private Transform _centrPosition;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Action<float> Activated;
    public Action Ended;

    private void Start()
    {
        _input = GetComponent<PlayerInputController>();
        _spriteRenderer.enabled = false;
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
        Gizmos.DrawWireSphere(_centrPosition.position, _radiusAction);
    }

    private void OnVamperismActevated()
    {
        Debug.Log("Вампиризм ^^");  
        StartCoroutine(VamperismActivating(_durationActiveTime));
    }

    private IEnumerator VamperismActivating(float duration)
    {
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
    }
}