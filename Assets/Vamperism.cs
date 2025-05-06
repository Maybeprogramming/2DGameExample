using UnityEngine;

public class Vamperism : MonoBehaviour
{
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private float _damage;
    [SerializeField] private float _durationActive;
    [SerializeField] private float _durationRecharge;
    [SerializeField] private float _radiusAction;
    [SerializeField] private Transform _centrPosition;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _input = GetComponent<PlayerInputController>();
        _spriteRenderer.enabled = false;
    }

    private void OnEnable()
    {
        _input.VamperismActivated += OnVamperismActevated;
    }

    private void OnDisable()
    {
        _input.VamperismActivated -= OnVamperismActevated;       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_centrPosition.position, _radiusAction);
    }

    private void OnVamperismActevated()
    {
        Debug.Log("Вампиризм ^^");  
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
    }
}