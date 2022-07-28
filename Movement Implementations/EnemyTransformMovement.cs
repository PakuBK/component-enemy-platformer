using UnityEngine;

public class EnemyTransformMovement : MonoBehaviour, IEnemyMovement
{
    public void Move(int direction, float movementSpeed) {
        transform.Translate((Vector3.right * direction) * movementSpeed * Time.deltaTime, Space.World);
    }
}
