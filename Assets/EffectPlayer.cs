using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _player;
    [SerializeField] private Health _health;

    private void Awake()
    {
        _health.Added += OnPlayHealingEffect;
    }

    private void OnPlayHealingEffect(float obj)
    {
        _player.Play();
    }
}