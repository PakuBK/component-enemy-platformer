using UnityEngine;

public class EnemyInteractionHandler : MonoBehaviour
{
    // Detect if Enemy touches Player
    // Detect if Player jumped on Enemy

    private Transform playerRef;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            HandlePlayerCollision(other.transform);
        }
    }

    private void HandlePlayerCollision(Transform player) {
        playerRef = player;
        // Check if Player is on top of Enemy
        if (player.position.y > transform.position.y){
            PlayerOnTop();
        }
        else {
            PlayerTouchesEnemy();
        }
    }

    private void PlayerOnTop() {
        Debug.Log("Player on Top of Enemy");
        gameObject.SetActive(false);
    }

    private void PlayerTouchesEnemy() {
        Debug.Log("Player touches Enemy");
        playerRef.gameObject.SetActive(false);
    }
}
