using UnityEngine;

public class EnemyVelocityMovement : MonoBehaviour, IEnemyMovement
{
    private Rigidbody2D rb;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(int direction, float movementSpeed) {
        rb.velocity = Vector2.right * direction * movementSpeed;
    }
}
