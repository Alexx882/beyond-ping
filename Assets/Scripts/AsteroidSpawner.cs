using Unity.Mathematics.Geometry;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject commander;
    public GameObject asteroidPrefab;
    public Transform[] spawns;
    public float rate;
    public float noise;

    public float force;
    public float forceNoise;

    public float angleNoise;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAsteroid();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (var spawn in spawns)
        {
            Gizmos.DrawWireSphere(spawn.transform.position, 1f);
        }
    }


    void SpawnAsteroid()
    {
        var index = Random.Range(0, spawns.Length);
        var spawn = spawns[index];

        var impulse = Random.Range(force - forceNoise, force + forceNoise);
        var directionToCommander = (commander.transform.position - spawn.position).normalized;
        
        var shootDirection = Quaternion.AngleAxis(Random.Range(-angleNoise, angleNoise), Vector3.up) * directionToCommander;
        
        
        var asteroid = Instantiate(asteroidPrefab, spawn.transform);
        var rigidBody = asteroid.GetComponentInChildren<Rigidbody2D>();
        rigidBody.AddForce(impulse * shootDirection, ForceMode2D.Impulse);
        rigidBody.AddTorque(Random.Range(-1, 1), ForceMode2D.Impulse);
        
        var nextSpawn = Random.Range(rate - noise, rate + noise);
        Invoke(nameof(SpawnAsteroid), nextSpawn);
    }
}
