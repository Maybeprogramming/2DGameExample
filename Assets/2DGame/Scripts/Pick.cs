using UnityEngine;

public class Pick : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health health);
        if (health != null)
        {
            health.TakeDamage(_damage);
        }
    }
}