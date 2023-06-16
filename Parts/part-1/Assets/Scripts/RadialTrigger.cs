using UnityEngine;
using UnityEditor;

public class RadialTrigger : MonoBehaviour
{
    [Range(0f, 4f)]
    public float radius = 1.0f;
    public Transform objectTransform;

    private void OnDrawGizmos()
    {
        // CheckRadialTriggerByGizmos();
        CheckRadialTriggerByHandles();
    }

    private void CheckRadialTriggerByGizmos()
    {
        Vector2 origin = transform.position;
        Vector2 objectPosition = objectTransform.position;

        float dist = Vector2.Distance(objectPosition, origin);
        bool isInside = dist < radius;

        Gizmos.color = isInside ? Color.green : Color.white;
        Gizmos.DrawWireSphere(origin, radius);
    }

    private void CheckRadialTriggerByHandles ()
    {
        Vector2 origin = transform.position;
        Vector2 objectPosition = objectTransform.position;

        float dist = Vector2.Distance(objectPosition, origin);
        bool isInside = dist < radius;

        Handles.color = isInside ? Color.green : Color.white;
        Handles.DrawWireDisc(origin, Vector3.forward, radius);
    }
}
