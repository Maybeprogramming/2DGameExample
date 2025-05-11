using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetectorByLayerMask : MonoBehaviour
{
    private const string EnemyLayer = "Enemy";

    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private List<Collider2D> _colliders;

    private void Awake()
    {
        _colliders = new List<Collider2D>();
        _collisionMask = LayerMask.GetMask(EnemyLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if ((_collisionMask.value & (1 << layer)) != 0)
        {
            _colliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _colliders.Remove(collision);
    }

    public bool TryGetNearestEnemyHealth(out Health enemyHealth)
    {
        List<Health> enemiesHealth = GetEnemiesHealth(_colliders);

        if (enemiesHealth.Count() > 0)
        {
            enemyHealth = enemiesHealth.OrderBy(healthEnemy =>
                (healthEnemy.transform.position - transform.position).sqrMagnitude).First();

            return true;
        }

        enemyHealth = null;
        return false;
    }

    private List<Health> GetEnemiesHealth(List<Collider2D> colliders) =>
       colliders.Select(collider => collider.GetComponent<Health>()).ToList();
}