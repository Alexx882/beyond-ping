using UnityEngine;

public class ConnectionLostCircleDrawer : MonoBehaviour
{
    public float radius = 60f;
    public Color gizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 center = transform.position;

        Gizmos.DrawWireSphere(center, radius);
    }
}
