using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputModule _input;
    
    public Vector2 Direction { get; private set; }

    public Action Attacked;
    public Action HeavyAttacked;
    public Action Jumped;
    public Action Walking;

    private void Awake()
    {
        _input = new InputModule();
    }

    private void Update()
    {
        Direction = _input.Player.Walk.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Attacked?.Invoke();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Attack.performed += OnAttack;
        _input.Player.HeavyAttack.performed += OnHeavyAttack;
        _input.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _input.Player.Attack.performed -= OnAttack;
        _input.Player.HeavyAttack.performed -= OnHeavyAttack;
        _input.Player.Jump.performed -= OnJump;
        _input.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jumped?.Invoke();
    }

    private void OnHeavyAttack(InputAction.CallbackContext context)
    {
        HeavyAttacked?.Invoke();
    }
}