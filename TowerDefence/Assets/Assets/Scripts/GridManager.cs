using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;

    public GameObject pathTile;

    private PathGenerator pathGenerator;

    void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        List<Vector2Int> pathBlocks = pathGenerator.GeneratePath();

        int pathSize = pathBlocks.Count;

        while (pathSize < minPathLength)
        {
            pathBlocks = pathGenerator.GeneratePath();
            pathSize = pathBlocks.Count;
        }
        
        
        StartCoroutine(LayPathBlocks(pathBlocks));
    }

    private IEnumerator LayPathBlocks (List<Vector2Int> pathBlocks)
    {
        foreach (Vector2Int pathBlock in pathBlocks)
        {
            Instantiate(pathTile, new Vector3(pathBlock.x, 0f, pathBlock.y), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
         yield return null;
    }
}
