using UnityEngine;

public class AutoMixPatrol : MonoBehaviour, IPatrolBehaviour
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

    [Header("Range Walk")]
    // RangeWalk
    [SerializeField] [Range(1f, 10f)] [Tooltip("Enemy Walking Range Left")]
    private float WalkRangeLeft = 1f;
    [SerializeField] [Range(1f, 10f)] [Tooltip("Enemy Walking Range Right")]
    private float WalkRangeRight = 1f;
    private Vector2 startPosition;

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

    private void Start() {
        startPosition = transform.position;
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

        Vector2 offset = new Vector2(transform.position.x + turnAroundDistance * direction, transform.position.y);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(offset, offset + Vector2.down * downRange);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallCheckRange * direction);
    }
    #endif
}
