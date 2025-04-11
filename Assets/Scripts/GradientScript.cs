using UnityEngine;

public class GradientScript : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public Transform fixedObject;      // Reference to the fixed object (e.g., the origin point like a star or planet)
    public SpriteRenderer backgroundRenderer;  // Reference to the SpriteRenderer for applying gradient
    public float maxDistance = 100f;   // Maximum distance to apply the gradient
    public Color centerColor = Color.yellow;  // Color at the center (the origin point)
    public Color edgeColor = Color.blue;      // Color at the edge (farthest from the origin)
    public LayerMask planetLayerMask;  // LayerMask to detect planets or obstacles

    private void Update()
    {
        // Calculate the distance between player and the fixed object (origin)
        Vector3 playerPosition = player.position;
        Vector3 originPosition = fixedObject.position;

        float distanceToOrigin = Vector3.Distance(playerPosition, originPosition);

        // Get the direction from player to fixed object
        Vector3 directionToOrigin = (originPosition - playerPosition).normalized;

        // Adjust the distance to take into account obstacles (planets or other objects)
        float adjustedDistance = GetAdjustedDistance(playerPosition, directionToOrigin);

        // Clamp the adjusted distance to maxDistance
        adjustedDistance = Mathf.Clamp(adjustedDistance, 0, maxDistance);

        // Calculate the color based on the distance
        Color gradientColor = Color.Lerp(edgeColor, centerColor, adjustedDistance / maxDistance);

        // Apply the calculated color to the background sprite (or any 2D object)
        backgroundRenderer.color = gradientColor;
    }

    private float GetAdjustedDistance(Vector3 playerPosition, Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, direction, maxDistance, planetLayerMask);

        // If there are obstacles (planets) between the player and the origin, adjust the distance
        foreach (RaycastHit2D hit in hits)
        {
            // The first hit should block the view to the origin (shadow effect)
            // For now, we simply use the distance to the first hit as the adjusted distance
            if (hit.collider != null)
            {
                return hit.distance;
            }
        }

        // If no obstacles are hit, return the full distance to the origin
        return Vector3.Distance(playerPosition, fixedObject.position);
    }
}
