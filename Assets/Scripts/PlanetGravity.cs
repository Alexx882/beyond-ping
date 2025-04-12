using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float planetMass = 2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplyGravityForMass();
    }
    void OnValidate()
    {
        ApplyGravityForMass();
    }

    /// <summary>
    /// Applies based on planet mass and transform scale
    /// </summary>
    private void ApplyGravityForMass()
    {
        var gravityScale = Mathf.Sqrt(transform.localScale.x);
        var radius = planetMass * gravityScale;
        
        Transform gravityRadius = transform.Find("GravityRadius");
        gravityRadius.localScale = new Vector3(radius, radius, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
