using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputModule _inputs;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool isRun = false;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isWalk = false;

    private void Awake()
    {
        _inputs = new InputModule();
        Debug.Log(_inputs == null);
        Debug.Log(_inputs.Player.Attack);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"Атака");
        _animator.SetTrigger("OnAttack");
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Player.Walk.performed += OnWalking;
        _inputs.Player.Attack.performed += OnAttack;
        _inputs.Player.Jump.performed += OnJump;
        _inputs.Player.HeavyAttack.performed += OnHeavyAttack;
        _inputs.Player.Run.performed += OnRunnig;
    }

    private void OnRunnig(InputAction.CallbackContext context)
    {
        Debug.Log($"Бег {context.ReadValue<float>()}");

        isRun = !isRun;
        _animator.SetBool("isRun", isRun);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Прыжок");
        isJump = !isJump;

        if (isJump)
        {
            _animator.SetTrigger("OnJump");
        }
        else
        {
            _animator.SetTrigger("OnGround");
        }
    }

    private void OnHeavyAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"Мега Атака");
        _animator.SetTrigger("OnHeavyAttack");
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
        isWalk = !isWalk;
        _animator.SetBool("isWalk", isWalk);
    }
}