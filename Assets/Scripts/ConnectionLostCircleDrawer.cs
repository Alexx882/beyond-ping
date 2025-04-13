using UnityEngine;

public class ConnectionLostCircleDrawer : MonoBehaviour
{
    public float lowConnectivity = 60f;
    public float lostConnectivity = 75f;
    public Color gizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 center = transform.position;

        Gizmos.DrawWireSphere(center, lowConnectivity);
        Gizmos.DrawWireSphere(center, lostConnectivity);
    }
}
