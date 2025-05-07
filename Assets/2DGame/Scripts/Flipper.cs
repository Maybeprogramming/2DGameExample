using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Vector3 _leftDirection = new(-1, 1, 1);
    [SerializeField] private Vector3 _rightDirection = new(1, 1, 1);

    public void Flip(Vector2 direction)
    {
        if (direction.x > 0f)
        {
            transform.localScale = _rightDirection;
        }
        else if (direction.x < 0f)
        {
            transform.localScale = _leftDirection;
        }
    }
}