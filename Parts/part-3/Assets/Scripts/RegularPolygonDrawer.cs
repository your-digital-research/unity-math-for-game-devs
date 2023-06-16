using UnityEngine;

public class RegularPolygonDrawer : MonoBehaviour
{
    [Range(3, 12)] public int sideCount = 3;
    [Range(1, 4)] public int dencity = 1;

    private const float TAU = 6.283185307179586f;
    private float DirectionToAngle(Vector2 direction) => Mathf.Atan2(direction.y, direction.x);
    private Vector2 AngleToDirection(float radian) => new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

    private void OnDrawGizmos()
    {
        DrawPolygon();
    }

    private void DrawPolygon()
    {
        Vector2[] vertices = new Vector2[sideCount];

        // Create vertices
        for (int i = 0; i < sideCount; i++)
        {
            vertices[i] = AngleToDirection(i * 2 * Mathf.PI / sideCount);
        }

        // Draw points
        for (int i = 0; i < sideCount; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

        // Draw lines
        for (int i = 0; i < sideCount; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[(i + dencity) % sideCount]);
        }
    }
}
