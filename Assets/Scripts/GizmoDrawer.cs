using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public float radius = 1.0f;

    void OnDrawGizmos()
    {
        // Set gizmo color
        Gizmos.color = Color.green;

        // Draw a wireframe sphere at the object's position
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
