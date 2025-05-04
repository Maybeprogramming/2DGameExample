using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<int> CoinAmountChanched;

    public int CoinAmount { get; private set; }

    private void Start()
    {
        CoinAmount = 0;
        CoinAmountChanched?.Invoke(CoinAmount);
    }

    public void AddCoin(int value)
    {
        CoinAmount = value > 0 ? CoinAmount += value : CoinAmount;
        CoinAmountChanched?.Invoke(CoinAmount);
    }
}