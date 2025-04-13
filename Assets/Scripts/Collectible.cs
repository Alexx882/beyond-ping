using System;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public void Collect()
    {
        GameStateScript gameState = this.GetComponentInParent<GameStateScript>();
        if (gameState is not null)
        {
            gameState.IncreaseCollectedCollectibles();
        }

        int i = gameState.collectibles.IndexOf(this.gameObject);
        TargetIndicator targetIndicator = gameState.targetIndicatorList[i];
        
        Destroy(targetIndicator.gameObject);
        Destroy(this.gameObject);
    }
}
