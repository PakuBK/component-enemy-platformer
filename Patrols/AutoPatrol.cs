using UnityEngine;

public class AutoPatrol : MonoBehaviour, IPatrolBehaviour
{
    [Header("Auto Walk")]
    [SerializeField] [Range(0,1f)] [Tooltip("Turn around threshhold")] 
    private float turnAroundDistance = 0.5f;
    [SerializeField] [Range(0.1f, 1f)]
    private float downRange = 0.5f;
    [SerializeField] 
    private LayerMask WhatIsGround;
    [SerializeField] [Range(0.1f,2f)] [Tooltip("Red Gizmos Line")]
    private float wallCheckRange;

    // Movement Implemenation
    private int direction = 1;
    [SerializeField] private float movementSpeed;
    private IEnemyMovement movement;

    private void Awake() {
        movement = GetComponent<IEnemyMovement>();
        if (movement == null) {
            Debug.LogError("Enemy Movement Component is Missing!");
        }
    }

    public void Patrol() {
        bool ShouldTurnAround() {
            Vector2 offset = new Vector2(transform.position.x + turnAroundDistance * direction, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(offset, Vector2.down, downRange, WhatIsGround);
            return hit.collider == null; // returns false if the raycast hits a ground object
        }

        if (ShouldTurnAround())
        {
            direction *= -1;
        }
        movement.Move(direction, movementSpeed);
    }

    public void HitWallBehaviour() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, wallCheckRange, WhatIsGround);
        if (hit.collider != null)
        {
            direction *= -1;
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Vector2 offset = new Vector2(transform.position.x + turnAroundDistance * direction, transform.position.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(offset, offset + Vector2.down * downRange);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallCheckRange * direction);
    }
    #endif
}
