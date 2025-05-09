using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputModule _input;
    
    public Vector2 Direction { get; private set; }

    public Action Attacked;
    public Action HeavyAttacked;
    public Action<InputAction.CallbackContext> Jumped;
    public Action VamperismActivated;

    private void Awake()
    {
        _input = new InputModule();
    }

    private void Update()
    {
        Direction = _input.Player.Walk.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Attack.performed += OnAttacked;
        _input.Player.HeavyAttack.performed += OnHeavyAttacked;
        _input.Player.Jump.performed += OnJumping;
        _input.Player.Vampirism.performed += OnVapirismActivated;
    }

    private void OnDisable()
    {
        _input.Player.Attack.performed -= OnAttacked;
        _input.Player.HeavyAttack.performed -= OnHeavyAttacked;
        _input.Player.Jump.performed -= OnJumping;
        _input.Player.Vampirism.performed -= OnVapirismActivated;
        _input.Disable();
    }

    public void Disable()
    {
        _input.Disable();
    }

    private void OnVapirismActivated(InputAction.CallbackContext context)
    {
        VamperismActivated?.Invoke();
    }

    private void OnAttacked(InputAction.CallbackContext context)
    {
        Attacked?.Invoke();
    }

    private void OnJumping(InputAction.CallbackContext context)
    {
        Jumped?.Invoke(context);
    }

    private void OnHeavyAttacked(InputAction.CallbackContext context)
    {
        HeavyAttacked?.Invoke();
    }
}