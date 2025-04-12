using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    public GameObject linePrefab;

    private GameStateScript gameStateScript;
    private List<LineRenderer> lines = new List<LineRenderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStateScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateScript>();

        foreach (var _ in gameStateScript.collectibleSpawnPositions) // always filled
        {
            GameObject lineObj = Instantiate(linePrefab);
            LineRenderer lr = lineObj.GetComponent<LineRenderer>();
            lines.Add(lr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var collectible in gameStateScript.collectibles)
        {
            var directionLine = lines[i];
            if (collectible.IsDestroyed())
            {
                directionLine.enabled = false;
            }
            else
            {
                // draw
                Vector2 direction = (collectible.transform.position - transform.position).normalized;
                float magnitude = direction.magnitude;

                Vector2 start = (Vector2)transform.position + (direction * .5f);
                Vector2 end = start + (direction * magnitude);

                directionLine.SetPosition(0, start);
                directionLine.SetPosition(1, end);
            }

            ++i;
        }
    }
}