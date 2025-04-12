using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    /// <summary>
    /// Spawns have to be child of GameState.
    /// </summary>
    public GameObject[] collectibleSpawnPositions;
    public GameObject collectiblePrefab;
    public int collectedCollectibles = 0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach  (var spawn in collectibleSpawnPositions)
        {
            Instantiate(collectiblePrefab, spawn.transform);
        }
    }

    public void IncreaseCollectedCollectibles()
    {
        collectedCollectibles++;
        if (collectedCollectibles == collectibleSpawnPositions.Length)
        {
            Debug.Log("Collected all");
        }
            
    }
}
