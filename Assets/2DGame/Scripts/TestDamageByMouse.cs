using UnityEngine;

public class TestDamageByMouse : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnMouseDown()
    {
        _health.Remove(1);
    }
}