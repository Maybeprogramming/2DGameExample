using System.Collections;
using UnityEngine;

public class Pick : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delayTimeSeconds;
    [SerializeField] private bool _doesDamaged;

    private Coroutine _damagingCoroutine;
    private Coroutine _delayTimerCoroutine;
    private WaitForSeconds _delay;
    [SerializeField] private bool _isWasDamage = false;
    [SerializeField] private int _count = 0;

    private void Start()
    {
        _delay = new WaitForSeconds(_delayTimeSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Enter");

        collision.gameObject.TryGetComponent<Health>(out Health health);

        if (health != null && _isWasDamage == false)
        {
            _doesDamaged = true;
            _isWasDamage = true;
            _damagingCoroutine = StartCoroutine(Damaging(health));
        }

        _delayTimerCoroutine = StartCoroutine(Countdown());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_damagingCoroutine != null)
        {
            _doesDamaged = false;
            StopCoroutine(_damagingCoroutine);
        }
    }

    private IEnumerator Damaging(Health health)
    {
        Debug.Log($"Корутина № {++_count}");

        while (_doesDamaged)
        {
            health.Remove(_damage);
            yield return _delay;
        }
    }

    private IEnumerator Countdown()
    {
        yield return _delayTimerCoroutine;

        _isWasDamage = false;
    }
}