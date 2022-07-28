using UnityEngine;

public class RangePatrol : MonoBehaviour, IPatrolBehaviour
{
    [Header("Range Walk")]
    // RangeWalk
    [SerializeField] [Range(1f, 10f)] [Tooltip("Enemy Walking Range Left")]
    private float WalkRangeLeft = 1f;
    [SerializeField] [Range(1f, 10f)] [Tooltip("Enemy Walking Range Right")]
    private float WalkRangeRight = 1f;
    private Vector2 startPosition;

    [SerializeField] 
    private LayerMask WhatIsGround;
    [SerializeField] [Range(0.1f,10f)] [Tooltip("Red Gizmos Line")]
    private float wallCheckRange;

    private int direction = 1;
    [SerializeField]
    private float movementSpeed;
    private IEnemyMovement movement;

    private void Awake() {
        movement = GetComponent<IEnemyMovement>();
        if (movement == null) {
            Debug.LogError("Enemy Movement Component is Missing!");
        }
    }

    private void Start() {
        startPosition = transform.position;
    }

    public void Patrol() {
        // Determine if we are right or left from the start
        float walkRange = (startPosition.x - transform.position.x) < 0 ? WalkRangeRight : WalkRangeLeft;
        if (Mathf.Abs(startPosition.x - transform.position.x) >= walkRange)
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
        Gizmos.color = Color.blue;
        if (Application.isPlaying) {
            Vector2 v1 = new Vector2(startPosition.x - WalkRangeLeft, transform.position.y);
            Vector2 v2 = new Vector2(startPosition.x + WalkRangeRight, transform.position.y);
            Gizmos.DrawLine(v1, v2);
        }
        else if (Application.isEditor) {
            Vector2 v1 = new Vector2(transform.position.x - WalkRangeLeft, transform.position.y);
            Vector2 v2 = new Vector2(transform.position.x + WalkRangeRight, transform.position.y);
            Gizmos.DrawLine(v1, v2);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallCheckRange * direction);
        
    }
    #endif
}
