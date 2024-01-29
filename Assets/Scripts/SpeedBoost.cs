using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedBoostFactor = 1.5f; // How much to boost the player speed
    public float speedBoostDuration = 2f; // How long the speed boost lasts

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) // Make sure to tag your player object with "Player"
        {
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(speedBoostFactor, speedBoostDuration);
            }
            Destroy(gameObject); // Optionally destroy the speed boost object
        }
    }
}
