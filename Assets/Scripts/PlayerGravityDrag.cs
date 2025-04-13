using System.Collections;
using UnityEngine;

public class PlayerGravityDrag : MonoBehaviour
{
    public float gravityConstant = 10f;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        yield return new WaitForFixedUpdate();
        
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("GravityRadius"))
        {
            ApplyPlanetGravity(otherGO);
        }
    }

    private void ApplyPlanetGravity(GameObject otherGO)
    {
        // inside a planet's gravity
        Vector2 direction = otherGO.transform.position - transform.position;
        float distanceSqr = direction.sqrMagnitude;

        if (distanceSqr == 0f) return;

        float forceMagnitude =
            gravityConstant * otherGO.GetComponentInParent<PlanetGravityRadius>().planetMass / distanceSqr;
        Vector2 force = direction.normalized * forceMagnitude;

        if (rb is not null)
            rb.AddForce(force);
    }
}