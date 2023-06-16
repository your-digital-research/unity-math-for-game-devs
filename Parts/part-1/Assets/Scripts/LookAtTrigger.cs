using UnityEngine;

public class LookAtTrigger : MonoBehaviour
{
    [Range(0f, 1f)]
    public float precisness = 0.5f;
    public Transform playerTransform;

    private void OnDrawGizmos()
    {
        CheckLookAtTrigger();
    }

    private void CheckLookAtTrigger()
    {
        Vector2 center = transform.position;
        Vector2 playerPosition = playerTransform.position;
        Vector2 playerLookDirection = playerTransform.right;
        Vector2 playerToTriggerDirection = (center - playerPosition).normalized;

        float dotProduct = Vector2.Dot(playerToTriggerDirection, playerLookDirection);
        bool isLooking = dotProduct >= precisness;

        Gizmos.color = isLooking ? Color.green : Color.white;
        Gizmos.DrawLine(playerPosition, playerPosition + playerToTriggerDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerPosition, playerPosition + playerLookDirection);
    }
}
