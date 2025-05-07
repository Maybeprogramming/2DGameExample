using System.Collections;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Flipper _flipperX;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private Vector2 _targetWaypoint;
    [SerializeField] private int _currentIndexWaypoint = 1;
    [SerializeField] private float _minDistanceToWaypoint = 0.1f;
    [SerializeField] private float _waitTimeInSeconds = 5;

    private WaitForSeconds _waitTime;
    private Coroutine _coroutine;

    private void Start()
    {
        _targetWaypoint = SetNextWaypoint();
        _waitTime = new WaitForSeconds(_waitTimeInSeconds);
        _coroutine = StartCoroutine(Moving());
    }

    private Vector2 SetNextWaypoint()
    {
        _currentIndexWaypoint = ++_currentIndexWaypoint % _waypoints.Length;
        float positionX = _waypoints[_currentIndexWaypoint].position.x;
        float positionY = _waypoints[_currentIndexWaypoint].position.y;
        return new Vector2(positionX, positionY);
    }

    private Vector2 GetDirection()
    {
        return new Vector2(_targetWaypoint.x - transform.position.x, Vector2.zero.y).normalized;
    }

    private IEnumerator Moving()
    {
        float distance;

        while (_health.IsAlive)
        {
            distance = Mathf.Abs(gameObject.transform.position.x - _targetWaypoint.x);

            if (distance >= _minDistanceToWaypoint)
            {
                transform.position = Vector2.MoveTowards(transform.position, _targetWaypoint, _patrolSpeed * Time.deltaTime);
                _flipperX.Flip(GetDirection());
                _animator.SetFloat("MoveDirection", Mathf.Abs(GetDirection().x)); //Магические параметры
            }
            else
            {
                _animator.SetFloat("MoveDirection", 0); //Магические параметры
                _targetWaypoint = SetNextWaypoint();
                yield return _waitTime;
            }

            yield return null;
        }
    }
}