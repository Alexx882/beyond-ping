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
            
        Destroy(this.gameObject);
    }
}
