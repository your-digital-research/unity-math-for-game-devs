using UnityEngine;

public class SpaceTransformation : MonoBehaviour
{
    public Transform childPointTransform;
    public Vector2 localSpacePoint = new Vector2(1, 1);
    public Vector2 worldSpacePoint = new Vector2(0, 0);

    private void OnDrawGizmos()
    {
        Vector2 objectPosition = transform.position;
        Vector2 right = transform.right;
        Vector2 up = transform.up;

        DrawBasisVectors(objectPosition, right, up);
        DrawBasisVectors(Vector2.zero, Vector2.right, Vector2.up);

        Vector2 worldSpacePosition = LocalToWorld(localSpacePoint, right, up);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(worldSpacePosition, 0.1f);

        childPointTransform.localPosition = WorldToLocal(worldSpacePoint, objectPosition, right, up);
    }

    private Vector2 LocalToWorld(Vector2 localPoint, Vector2 right, Vector2 up)
    {
        Vector2 worldOffset = right * localPoint.x + up * localPoint.y;
        Vector2 position = (Vector2)transform.position + worldOffset;

        return position;
    }

    private Vector2 WorldToLocal(Vector2 worldPoint, Vector2 objectPosition, Vector2 right, Vector2 up)
    {
        Vector2 relativePoint = worldPoint - objectPosition;
        float x = Vector2.Dot(relativePoint, right);
        float y = Vector2.Dot(relativePoint, up);

        return new Vector2(x, y);
    }

    private void DrawBasisVectors(Vector2 position, Vector2 right, Vector2 up)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, up);
        Gizmos.color = Color.white;
    }
}
