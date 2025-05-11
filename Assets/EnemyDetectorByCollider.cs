using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetectorByCollider : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private int _enemyLayer;
    [SerializeField] private List<Collider2D> _collider;

    private void Awake()
    {
        _collider = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false)
        {
        }

        Debug.Log($"!{collision.includeLayers.value} - {_enemyMask.value}");
        _collider.Add(collision);
        Debug.Log($"{collision.gameObject.layer} - {_enemyMask.value}");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _collider.Remove(collision);
    }
}