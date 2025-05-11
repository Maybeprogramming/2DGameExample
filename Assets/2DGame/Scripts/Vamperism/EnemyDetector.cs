using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _radiusAction;
    [SerializeField] private Transform _vamperismPosition;
    [SerializeField] private LayerMask _layerMask;

    private void OnDrawGizmos() =>
        Gizmos.DrawWireSphere(_vamperismPosition.position, _radiusAction);

    public bool TryGetEnemyHealth(out Health enemyHealth)
    {
        Collider2D[] colliders = GetEnemyColliders();
        Health[] enemiesHealth = GetEnemiesHealth(colliders);

        if (enemiesHealth.Count() > 0)
        {
            enemyHealth = enemiesHealth?.OrderBy(healthEnemy =>
                (healthEnemy.transform.position - transform.position).sqrMagnitude).First();

            return true;
        }

        enemyHealth = null;
        return false;
    }

    private Health[] GetEnemiesHealth(Collider2D[] colliders) =>
       colliders.Select(collider => collider.GetComponent<Health>()).ToArray();

    private Collider2D[] GetEnemyColliders() =>
     Physics2D.OverlapCircleAll(_vamperismPosition.position, _radiusAction, _layerMask).
         Where(collider => collider.TryGetComponent<Enemy>(out Enemy enemy)).ToArray();
}