using UnityEngine;

public class FlipperX : MonoBehaviour
{
    public void Flip(Vector2 direction)
    {
        if (direction.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}