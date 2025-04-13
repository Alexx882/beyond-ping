using System;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    /// <summary>
    /// Spawns have to be child of GameState.
    /// </summary>
    public GameObject[] collectibleSpawnPositions;
    
    public GameObject commander;

    public GameObject ship;
    public List<TargetIndicator> targetIndicatorList;

    public GameObject collectiblePrefab;
    public GameObject indicatorPrefab;
    public int collectedCollectibles = 0;

    public List<GameObject> collectibles = new List<GameObject>();

    public bool CollectedAllCollectibles => collectedCollectibles == collectibleSpawnPositions.Length;
    public bool hasWon = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        foreach (var spawn in collectibleSpawnPositions)
        {
            GameObject target = Instantiate(collectiblePrefab, spawn.transform);
            collectibles.Add(target);
            TargetIndicator indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();
            indicator.target = target.transform;
            indicator.transform.parent = ship.transform;
            indicator.transform.position = ship.transform.position;
            targetIndicatorList.Add(indicator);
        }
    }

    public void Reset()
    {
        foreach (var collectible in collectibles)
        {
            if (!collectible.IsDestroyed())
                Destroy(collectible);
        }
        foreach (var targetIndicator in targetIndicatorList)
        {
            if (!targetIndicator.IsDestroyed())
                Destroy(targetIndicator.gameObject);
        }

        hasWon = false;
        collectedCollectibles = 0;
        collectibles = new List<GameObject>();
        targetIndicatorList = new List<TargetIndicator>();
        SpawnCollectibles();
    }

    public void IncreaseCollectedCollectibles()
    {
        collectedCollectibles++;
        if (CollectedAllCollectibles)
        {
            // add indicator back to commander
            TargetIndicator indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();
            indicator.target = commander.transform;
            indicator.transform.parent = ship.transform;
            indicator.transform.position = ship.transform.position;
            targetIndicatorList.Add(indicator);
        }
    }

    public void ReturnedToCommander()
    {
        if (CollectedAllCollectibles)
        {
            hasWon = true;
        }
    }
}