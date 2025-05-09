using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Flipper _flipperX;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _flipperX.Flip(_playerInputController.Direction);
        float newPositionX = transform.position.x + _playerInputController.Direction.x * Time.deltaTime * _walkSpeed;
        transform.position = new Vector3(newPositionX, transform.position.y, 0);
    }
}