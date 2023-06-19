using UnityEngine;
using UnityEditor;

public class InverseLerp : MonoBehaviour
{ 
    public Transform point;
    public float volume = 0f;
    [Range(0f, 2.5f)] public float innerRadius = 2.5f;
    [Range(2.5f, 5f)] public float outerRadius = 5f;

    private void OnDrawGizmos()
    {
        DrawInverseLerpVisualisation();
    }

    private void DrawInverseLerpVisualisation()
    {
        Handles.DrawWireDisc(Vector3.zero, Vector3.up, innerRadius);
        Handles.DrawWireDisc(Vector3.zero, Vector3.up, outerRadius);

        Vector3 pointPosition = point.position;
        Gizmos.DrawSphere(pointPosition, 0.1f);

        float distanceToPoint = Vector3.Distance(pointPosition, Vector3.zero);
        volume = Mathf.InverseLerp(outerRadius, innerRadius, distanceToPoint);

        Gizmos.DrawLine(pointPosition, pointPosition + Vector3.up * volume);
    }
} 
