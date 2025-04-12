using UnityEngine;


public class PlayerGravityDrag : MonoBehaviour
{
    public float gravityStrength = 10f;
    public float gravityRange = 50f; // Max range planets affect the object
    public string planetLayerName = "Planet"; // or "Obstacle" if you decide not to change layers

    private Rigidbody2D rb;
    private int planetLayerMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        planetLayerMask = 1 << LayerMask.NameToLayer(planetLayerName); // bitmask for OverlapCircleAll    }
    }

    void FixedUpdate()
    {
        Collider2D[] planetColliders = Physics2D.OverlapCircleAll(rb.position, gravityRange, planetLayerMask);

        Debug.Log(planetColliders.Length);
        return;
        // Vector2 totalGravity = Vector2.zero;
        //
        // foreach (GameObject obj in allObjects)
        // {
        //     if (obj.layer != planetLayer) continue;
        //
        //     Vector2 direction = (Vector2)obj.transform.position - rb.position;
        //     float distanceSqr = direction.sqrMagnitude;
        //
        //     if (distanceSqr == 0) continue; // avoid division by zero
        //
        //     // Inverse square gravity
        //     Vector2 force = direction.normalized * (gravityStrength / distanceSqr);
        //     totalGravity += force;
        // }
        //
        // rb.AddForce(totalGravity);
    }

}
