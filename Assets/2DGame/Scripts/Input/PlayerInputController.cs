using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputControls _inputs;

    private void Awake()
    {
        _inputs = new InputControls();
    }

    private void Start()
    {     
        _inputs.Player.Walk.performed += OnWalking;
        _inputs.Player.Attack.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log($"{context}");
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _inputs.Player.Walk.performed -= OnWalking;
        _inputs.Player.Attack.performed -= Attack;

    }

    private void OnWalking(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(callbackContext.ReadValue<float>());
    }

    private void Update()
    {

    }
}