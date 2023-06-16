using UnityEngine;

public class MeshSurfaceArea : MonoBehaviour
{
    public Mesh mesh;
    public float area = 0.0f;

    private void OnValidate()
    {
        CalculateArea();
    }

    private void OnDrawGizmos()
    {
        DrawVertices();
    }

    private void CalculateArea()
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        area = 0.0f;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            int aIndex = triangles[i];
            int bIndex = triangles[i + 1];
            int cIndex = triangles[i + 2];

            Vector3 a = vertices[aIndex];
            Vector3 b = vertices[bIndex];
            Vector3 c = vertices[cIndex];

            area += Vector3.Cross(b - a, c - a).magnitude;
        }

        area /= 2;
    }

    private void DrawVertices()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), 0.025f);
        }
    }
}
