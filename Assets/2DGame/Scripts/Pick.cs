using System.Collections;
using UnityEngine;

public class Pick : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _frequencyDamageInSec;
    [SerializeField] private bool _isWork;

    private Coroutine _coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health health);

        if (health != null)
        {
            _isWork = true;
            _coroutine = StartCoroutine(Damaging(health));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_coroutine != null)
        {
            _isWork = false;
            StopCoroutine(nameof(Damaging));
        }
    }

    private IEnumerator Damaging(Health health)
    {
        float elapsedTime = 0;
        float timeSecond = 1;
        float tickTime = timeSecond / _frequencyDamageInSec;

        health.TakeDamage(_damage);

        while (_isWork)
        {
            if (elapsedTime > tickTime)
            {
                health.TakeDamage(_damage);
                elapsedTime = 0;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}