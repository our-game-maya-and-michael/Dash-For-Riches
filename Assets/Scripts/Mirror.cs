using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // How much to slow the player down
    public float slowDownDuration = 2f; // How long the slow down lasts

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure to tag your player object with "Player"
        {
            Player playerMovement = collision.gameObject.GetComponent<Player>();
            if (playerMovement != null)
            {
                playerMovement.ApplySlow(slowDownFactor, slowDownDuration);
            }
            // Add effect for mirror breaking here if needed
            Destroy(gameObject); // Destroy the mirror object
        }
    }
}
