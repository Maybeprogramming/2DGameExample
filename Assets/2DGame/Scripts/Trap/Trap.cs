using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delayTimeSeconds;
    [SerializeField] private bool _doesDamaged;

    private Coroutine _cyclicalDamaging;
    private Coroutine _delayNewDamaging;
    private WaitForSeconds _delaySeconds;
    private bool _isWasDamage = false;

    private void Start()
    {
        _delaySeconds = new WaitForSeconds(_delayTimeSeconds);
        _doesDamaged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health health);

        if (health != null && _isWasDamage == false)
        {
            _isWasDamage = true;
            _cyclicalDamaging = StartCoroutine(Damaging(health));
            _delayNewDamaging = StartCoroutine(Countdown());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_cyclicalDamaging != null)
            StopCoroutine(_cyclicalDamaging);
    }

    private IEnumerator Damaging(Health health)
    {
        while (_doesDamaged && health.IsAlive)
        {
            health.Remove(_damage);
            yield return _delaySeconds;
        }
    }

    private IEnumerator Countdown()
    {
        yield return _delaySeconds;

        _isWasDamage = false;

        yield return null;
    }
}