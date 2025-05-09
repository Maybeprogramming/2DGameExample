using UnityEngine;

public class RendererVamperismArea : MonoBehaviour
{
    [SerializeField] private Vamperism _vamperism;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake() =>    
        _spriteRenderer.enabled = false;    

    private void OnEnable()
    {
        _vamperism.Activated += OnActivatedRenderer;
        _vamperism.Ended += OnDisActivateRenderer;
    }

    private void OnDisable() =>    
        _vamperism.Activated -= OnActivatedRenderer;    

    private void OnActivatedRenderer(float durationTime) =>    
        _spriteRenderer.enabled = true;    

    private void OnDisActivateRenderer() =>    
        _spriteRenderer.enabled = false;
    
}