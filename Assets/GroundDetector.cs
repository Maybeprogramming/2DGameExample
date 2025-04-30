using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _grounDetecor;
    [SerializeField] private float _radiusDetection;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGrounded => HasGrounded();

    private Vector2 _point;

    private bool HasGrounded()
    {
        SetDetectorPointPosition();
        bool isGround = Physics2D.OverlapCircle(_point, _radiusDetection, _groundMask);
        return isGround;
    }

    private void OnDrawGizmos()
    {
        SetDetectorPointPosition();
        Gizmos.DrawWireSphere(_point, _radiusDetection);
    }

    private void SetDetectorPointPosition()
    {
        _point = new Vector2(_grounDetecor.transform.position.x, _grounDetecor.transform.position.y);
    }
}