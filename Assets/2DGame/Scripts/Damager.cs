using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private Transform _pointDetectorEnemy;
    [SerializeField] private float _radiusDetectorEnemy;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _damageHeavy;

    private void HeavyAttack()
    {
        TakeAttak(_damageHeavy);
    }

    private void TakeAttak(float damage)
    {
        Collider2D enemy = Physics2D.OverlapCircle(_pointDetectorEnemy.position, _radiusDetectorEnemy, _enemyMask);

        if (enemy != null)
        {
            enemy.TryGetComponent<Health>(out Health health);
            health.TakeDamage(damage);
        }
    }

    private void Attack()
    {
        TakeAttak(_damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_pointDetectorEnemy.position, _radiusDetectorEnemy);
    }
}