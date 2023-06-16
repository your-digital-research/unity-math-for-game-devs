using UnityEngine;

public class Lerp : MonoBehaviour
{
    [Range(0f, 1f)]
    public float t = 0.5f;

    public Transform firstPoint;
    public Transform secondPoint;

    private void OnDrawGizmos()
    {
        DrawPoints();
    }

    private void DrawPoints()
    {
        Vector3 firstPointPosition = firstPoint.position;
        Vector3 secondPointPosition = secondPoint.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(firstPointPosition, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(secondPointPosition, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(firstPointPosition, secondPointPosition);

        Color thirdPointColor = Color.Lerp(Color.blue, Color.red, t);
        Vector3 thirdPointPosition = Vector3.Lerp(firstPointPosition, secondPointPosition, t);

        Gizmos.color = thirdPointColor;
        Gizmos.DrawSphere(thirdPointPosition, 0.1f);
    }
}
