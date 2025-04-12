using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    /// <summary>
    /// Spawns have to be child of GameState.
    /// </summary>
    public GameObject[] collectibleSpawnPositions;

    public GameObject collectiblePrefab;
    public int collectedCollectibles = 0;

    public List<GameObject> collectibles = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        foreach (var spawn in collectibleSpawnPositions)
        {
            collectibles.Add(Instantiate(collectiblePrefab, spawn.transform));
        }
    }

    public void Reset()
    {
        foreach (var collectible in collectibles)
        {
            if (!collectible.IsDestroyed())
                Destroy(collectible);
        }

        collectedCollectibles = 0;
        collectibles = new List<GameObject>();
        SpawnCollectibles();
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