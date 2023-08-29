using UnityEngine;
using UnityEditor;

public class Coil : MonoBehaviour
{
    [Range(1f, 100f)] public int numberOfTurns = 25;
    [Range(1f, 50f)] public int coilHeight = 5;
    [Range(1f, 50f)] public int coilRadius = 1;
    [Range(1f, 25f)] public int torusRadius = 25;
    [Range(32f, 256f)] public int pointsPerTurn = 128;

    public Color colorFrom = Color.red;
    public Color colorTo = Color.blue;

    private void OnDrawGizmos()
    {
        DrawLinearCoil();
        // DrawTorusCoil();
    }

    private void DrawLinearCoil()
    {
        int pointCount = numberOfTurns * pointsPerTurn;
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            float height = i / (pointCount - 1f);
            points[i] = GetLinearCoilPoint(height);
        }

        DrawPoints(pointCount, points);
    }

    private void DrawTorusCoil()
    {
        int pointCount = numberOfTurns * pointsPerTurn;
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            float height = i / (pointCount - 1f);
            points[i] = GetTorusCoilPoint(height);
        }

        DrawPoints(pointCount, points);
    }

    private void DrawPoints(int pointCount, Vector3[] points)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            float t = i / (pointCount - 1f);
            Color color = Color.Lerp(colorFrom, colorTo, t);

            Handles.color = color;
            Handles.DrawAAPolyLine(5f, points[i], points[i + 1]);
        }
    }

    private Vector3 GetLinearCoilPoint(float height)
    {
        float winding = height * numberOfTurns;
        float angle = winding * Mathf.PI * 2;

        Vector3 point = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * coilRadius;
        point.y = height * coilHeight;

        return point;
    }

    private Vector3 GetTorusCoilPoint(float height)
    {
        float corePointAngle = height * Mathf.PI * 2;
        Vector3 corePoint = new Vector3(Mathf.Cos(corePointAngle), Mathf.Sin(corePointAngle), 0) * torusRadius;

        float winding = height * numberOfTurns;
        float coilPontAngle = winding * Mathf.PI * 2;

        Vector3 localPoint = new Vector3(Mathf.Cos(coilPontAngle), Mathf.Sin(coilPontAngle), 0) * coilRadius;
        Vector3 localX = corePoint.normalized;
        Vector3 localY = Vector3.forward;

        return corePoint + (localPoint.x * localX) + (localPoint.y * localY);
    }
}
