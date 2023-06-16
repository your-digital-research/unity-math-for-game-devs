using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    [Range(0f, 1f)] public float arcRadius = 0.5f;
    [Range(0f, 0.5f)] public float[] itemRadiuses = new float[] { 0.05f, 0.05f, 0.05f, 0.05f, 0.05f };

    private void OnDrawGizmos()
    {
        DrawArcWithCircles();
    }

    private void DrawArcWithCircles()
    {
        using (new Handles.DrawingScope(transform.localToWorldMatrix))
        {
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, 45, arcRadius);
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, -45, arcRadius);

            float totalAngle = 0f;
            int itemCount = itemRadiuses.Length;
            float[] anglesBetween = new float[itemCount - 1];

            for (int i = 0; i < anglesBetween.Length; i++)
            {
                float firstRadius = itemRadiuses[i];
                float secondRadius = itemRadiuses[i + 1];
                float radiusSummary = firstRadius + secondRadius;
                float angle = Mathf.Acos(1f - (radiusSummary * radiusSummary) / (2 * arcRadius * arcRadius));

                totalAngle += angle;
                anglesBetween[i] = angle;
            }


            float startAngle = totalAngle / 2;

            for (int i = 0; i < itemCount; i++)
            {
                float radius = itemRadiuses[i];
                Vector3 itemCenter = new Vector3(Mathf.Cos(startAngle), Mathf.Sin(startAngle), 0) * arcRadius;
                Handles.DrawWireDisc(itemCenter, Vector3.forward, radius);

                if (i < itemCount - 1)
                {
                    startAngle -= anglesBetween[i];
                }
            }
        }
    }
}
