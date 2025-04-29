using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputController : MonoBehaviour
{
    private InputModule _inputs;

    private void Awake()
    {
        _inputs = new InputModule();
        Debug.Log(_inputs == null);
        Debug.Log(_inputs.Player.Attack);
    }

    private void Start()
    {
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"Атака");
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Player.Walk.performed += OnWalking;
        _inputs.Player.Attack.performed += OnAttack;
        _inputs.Player.Jump.performed +=  OnJump;
        _inputs.Player.HeavyAttack.performed += OnHeavyAttack;
        _inputs.Player.Run.performed += OnRunnig;
    }

    private void OnRunnig(InputAction.CallbackContext context)
    {
        Debug.Log($"Бег {context.ReadValue<float>()}");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Прыжок");
    }

    private void OnHeavyAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"Мега Атака");
    }

    private void OnDisable()
    {
        _inputs.Player.Walk.performed -= OnWalking;
        _inputs.Player.Attack.performed -= OnAttack;
        _inputs.Disable();

    }

    private void OnWalking(InputAction.CallbackContext context)
    {
        Debug.Log("Ходьба");
    }
}