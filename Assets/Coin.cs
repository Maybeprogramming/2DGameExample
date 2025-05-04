using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Wallet>(out Wallet wallet);

        if (wallet != null)
        {
            wallet.AddCoin(_amount);
            gameObject.SetActive(false);
        }
    }
}