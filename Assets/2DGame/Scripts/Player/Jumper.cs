using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundDetector))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private PlayerInputController _controller;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private float _targetTime;
    [SerializeField] private float _heihtJump;
    [SerializeField] private CeilingDetector _ceilingDetector;

    private Coroutine _coroutine;

    public Vector2 Position => transform.position;

    private void OnEnable()
    {
        _controller.Jumped += OnJump;
        _ceilingDetector.Detected += OnCeilingDetect;
    }

    private void OnDisable()
    {
        _controller.Jumped -= OnJump;
        _ceilingDetector.Detected -= OnCeilingDetect;
    }

    private void OnCeilingDetect()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_detector.IsGrounded && context.action.IsPressed() == true)
            _coroutine = StartCoroutine(Jumping(_targetTime, context));
    }

    private IEnumerator Jumping(float targetTime, InputAction.CallbackContext context)
    {
        float timeElapsed = 0f;
        float currentPosition = Position.y;
        float maxJumpPosition = currentPosition + Vector2.up.y * _heihtJump;
        float distance = maxJumpPosition - currentPosition;
        float currentDistance;

        while (timeElapsed < targetTime && context.action.IsPressed())
        {
            timeElapsed += Time.deltaTime;
            currentDistance = timeElapsed / targetTime * distance;
            transform.position = Vector2.MoveTowards(new Vector2(Position.x, currentPosition), new Vector2(Position.x, maxJumpPosition), currentDistance);

            yield return null;
        }
    }
}