using UnityEngine;

public class InputSwitchOff : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private PlayerInputController _input;

    private void Start()
    {
        _health = GetComponent<Health>();
        _input = GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        _health.Dead += OnSwitchOffInput;
    }

    private void OnDisable()
    {
        _health.Dead -= OnSwitchOffInput;
    }

    private void OnSwitchOffInput()
    {
        _input.Disable();
    }
}