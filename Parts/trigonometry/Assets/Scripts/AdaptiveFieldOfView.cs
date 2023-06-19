using UnityEngine;

public class AdaptiveFieldOfView : MonoBehaviour
{
    public Point[] points;
    public Transform[] pointsTransforms;


    private void OnDrawGizmos()
    {
        SetFieldOfViewByLowestDot();
        // SetFieldOfViewByHighestAngle();
        // SetFieldOfViewDependOnCameraRotation();

        DrawPointsRadiuses();
    }

    private void SetFieldOfViewByLowestDot()
    {
        float lowestDot = float.MaxValue;
        Camera camera = GetComponent<Camera>();

        Vector2 outermostPosition = default;
        Vector2 cameraPosition = camera.transform.position;
        Vector2 cameraDirection = camera.transform.forward;

        foreach (Transform pointTransform in pointsTransforms)
        {
            Vector2 point = (Vector2)pointTransform.position - cameraPosition;
            Vector2 directionToPoint = point.normalized;

            float dot = Vector2.Dot(cameraDirection, directionToPoint);

            if (dot < lowestDot)
            {
                lowestDot = dot;
                outermostPosition = pointTransform.position;
            }
        }

        Gizmos.DrawLine(cameraPosition, outermostPosition);

        float angleInRadians = Mathf.Acos(lowestDot);
        camera.fieldOfView = 2 * angleInRadians * Mathf.Rad2Deg;
    }

    private void SetFieldOfViewByHighestAngle()
    {
        float highestAngle = float.MinValue;
        Camera camera = GetComponent<Camera>();

        Vector2 outermostPosition = default;
        Vector2 cameraPosition = camera.transform.position;
        Vector2 cameraDirection = camera.transform.forward;

        foreach (Point pointComponent in points)
        {
            Vector2 point = (Vector2)pointComponent.transform.position - cameraPosition;
            Vector2 directionToPoint = point.normalized;

            float radius = pointComponent.radius;
            float distanceToPoint = point.magnitude;
            float dot = Vector2.Dot(cameraDirection, directionToPoint);

            float angleToPoint = Mathf.Acos(dot);
            float radiusAngularSpan = Mathf.Asin(radius / distanceToPoint);
            float angluarDeviation = angleToPoint + radiusAngularSpan;

            if (angluarDeviation > highestAngle)
            {
                highestAngle = angluarDeviation;
                outermostPosition = pointComponent.transform.position;
            }
        }

        Gizmos.DrawLine(cameraPosition, outermostPosition);

        camera.fieldOfView = 2 * highestAngle * Mathf.Rad2Deg;
    }

    private void SetFieldOfViewDependOnCameraRotation()
    {
        Vector3 center = Vector3.zero;
        float highestAngle = float.MinValue;
        Camera camera = GetComponent<Camera>();
        Vector2 cameraDirection = Vector2.right;

        foreach (Point point in points)
        {
            center += point.transform.position;
        }

        center /= points.Length;
        Vector3 directionToCenter = center - camera.transform.position;
        camera.transform.rotation = Quaternion.LookRotation(directionToCenter, camera.transform.up);

        foreach (Point point in points)
        {
            Vector3 pointLocal = camera.transform.InverseTransformPoint(point.transform.position);
            Vector2 pointFlat = new Vector2(pointLocal.z, pointLocal.y);
            Vector2 directionToPoint = pointFlat.normalized;

            float radius = point.radius;
            float distanceToPoint = pointFlat.magnitude;
            float dot = Vector2.Dot(cameraDirection, directionToPoint);

            float angleToPoint = Mathf.Acos(dot);
            float radiusAngularSpan = Mathf.Asin(radius / distanceToPoint);
            float angluarDeviation = angleToPoint + radiusAngularSpan;

            if (angluarDeviation > highestAngle)
            {
                highestAngle = angluarDeviation;
            }
        }

        camera.fieldOfView = 2 * highestAngle * Mathf.Rad2Deg;
    }

    public void DrawPointsRadiuses()
    {
        foreach (Point point in points)
        {
            Gizmos.DrawWireSphere(point.transform.position, point.radius);
        }
    }
}
