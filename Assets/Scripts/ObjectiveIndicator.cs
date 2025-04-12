using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    private GameStateScript gameStateScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStateScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateScript>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var collectible in gameStateScript.collectibles)
        {
             (collectible.transform.position - this.transform.position).magnitude;

        }
    }
}
