using UnityEngine;
using UnityEditor;

public class TheTurret : MonoBehaviour
{
    private void DrawRay(Vector3 point, Vector3 direction) => Handles.DrawAAPolyLine(point, point + direction);

    private void DrawCubePoints(Matrix4x4 matrix)
    {
        Vector3[] points = new Vector3[]
        {
            // Bottom 4 positions
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, -0.5f),

            // Top 4 positions
            new Vector3(0.5f, 1, 0.5f),
            new Vector3(-0.5f, 1, 0.5f),
            new Vector3(-0.5f, 1, -0.5f),
            new Vector3(0.5f, 1, -0.5f)
        };

        Gizmos.color = Color.cyan;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 worldPoint = matrix.MultiplyPoint3x4(points[i]);
            Gizmos.DrawSphere(worldPoint, 0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pointPosition = transform.position;
        Vector3 lookDirection = transform.forward;

        if (Physics.Raycast(pointPosition, lookDirection, out RaycastHit hit))
        {
            Vector3 hitPosition = hit.point;
            Vector3 normal = hit.normal;
            Vector3 right = Vector3.Cross(normal, lookDirection).normalized;
            Vector3 forward = Vector3.Cross(right, normal);

            // Turret Rotation
            Quaternion turretRotation = Quaternion.LookRotation(forward, normal);

            // Turret Matrix
            Matrix4x4 turretToWorldMatrix = Matrix4x4.TRS(hitPosition, turretRotation, Vector3.one);

            // Draw Cube Points
            DrawCubePoints(turretToWorldMatrix);

            // Draw hit point
            Handles.color = Color.black;
            Handles.DrawSolidDisc(hitPosition, normal, 0.1f);

            // Draw hit vector
            Handles.color = Color.white;
            Handles.DrawAAPolyLine(pointPosition, hitPosition);

            // Draw normal vector
            Handles.color = Color.green;
            DrawRay(hitPosition, normal);

            // Draw right vector
            Handles.color = Color.red;
            DrawRay(hitPosition, right);

            // Draw forward vector
            Handles.color = Color.blue;
            DrawRay(hitPosition, forward);
        } else
        {
            // Draw look direction vector
            Handles.color = Color.white;
            DrawRay(pointPosition, lookDirection);
        }
    }
}
