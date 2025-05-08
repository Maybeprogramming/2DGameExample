using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        float positionX = _target.position.x + _offset.x;
        float positionY = _target.position.y + _offset.y;
        transform.position = new Vector2(positionX, positionY);
    }
}