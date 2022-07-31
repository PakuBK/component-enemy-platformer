using UnityEngine;

public class EnemyTransformMovement : MonoBehaviour, IEnemyMovement
{
    private Rigidbody2D rb;
    private Collider2D cld;
    [SerializeField]
    private LayerMask ground;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<Collider2D>();
    }

    public void Move(int direction, float movementSpeed) {
        transform.Translate((Vector3.right * direction) * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (cld.IsTouchingLayers(ground))
        {
            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
            }
        }
    }
}
