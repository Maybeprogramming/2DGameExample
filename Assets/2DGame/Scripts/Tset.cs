using UnityEngine;

public class Tset : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnMouseDown()
    {
        _health.Remove(1);
    }
}