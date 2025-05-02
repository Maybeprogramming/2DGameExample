using UnityEngine;

public class Damager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health health);
        health?.TakeDamage(4);
    }
}