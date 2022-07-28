using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
    Component Based Enemy System
    
    Required Components
    [Enemy Controller <EnemyController>]
    [Patrol Behaviour <IPatrolBehaviour>]
    [Movement Implementation <IEnemyMovement>]
    */
    private IPatrolBehaviour patrol;

    private void Awake() {
        patrol = GetComponent<IPatrolBehaviour>();
        if (patrol == null) {
            Debug.LogError("Enemy Patrol Script is Missing!");
        }
    }
    
    private void Update() {
        patrol.HitWallBehaviour();
        patrol.Patrol();
    }
}
