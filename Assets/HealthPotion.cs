using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float _healthPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Health>(out Health health);

        if (health != null)
        {
            health.Add(_healthPoint);
            gameObject.SetActive(false);
        }
    }
}