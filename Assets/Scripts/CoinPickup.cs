using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int moneyValue = 1; // Amount of money this coin gives

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.AddMoney(moneyValue);
            Destroy(gameObject); // Destroy the coin after it's picked up
        }
    }
}