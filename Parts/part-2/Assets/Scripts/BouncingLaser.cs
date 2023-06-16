using UnityEngine;

public class BouncingLaser : MonoBehaviour
{
    public int maxBounceCount = 3;

    private void DrawRay(Vector3 point, Vector3 direction) => Gizmos.DrawLine(point, point + direction);

    private void OnDrawGizmos()
    {
        DrawReflectedVector();
    }

    private void DrawReflectedVector()
    {
        int bounces = 0;
        Vector3 position = transform.position;
        Vector3 lookDirection = transform.forward;

        while (bounces < maxBounceCount)
        {
            if (Physics.Raycast(position, lookDirection, out RaycastHit hit))
            {
                Vector3 normal = hit.normal;
                Vector3 hitPosition = hit.point;

                // Draw hit point
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(hitPosition, 0.1f);

                // Draw normal vector
                Gizmos.color = Color.blue;
                DrawRay(hitPosition, normal);

                // Draw hit vector
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, hitPosition);

                // Draw reflected vector
                Vector3 reflectedVector = lookDirection - 2 * Vector3.Dot(normal, lookDirection) * normal;
                Gizmos.color = Color.cyan;
                DrawRay(hitPosition, reflectedVector);

                // Reassign position and look direction vectors
                position = hitPosition;
                lookDirection = reflectedVector;
            }
            else
            {
                // Draw look direction vector
                Gizmos.color = Color.white;
                DrawRay(position, lookDirection);
            }

            // Increase bounces count
            bounces += 1;
        }
    }
}
