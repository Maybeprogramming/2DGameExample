using TMPro;
using UnityEngine;

public class CoinShower : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro.text = $"x {_wallet.CoinAmount.ToString()}";
    }

    private void OnEnable()
    {
        _wallet.CoinAmountChanched += OnCoinChanged;
    }

    private void OnDisable()
    {
        _wallet.CoinAmountChanched -= OnCoinChanged;        
    }

    private void OnCoinChanged(int value)
    {
        _textMeshPro.text = $"x {value.ToString()}";
    }
}