using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;

    public GridBlockObject[] pathBlockObject;
    public GridBlockObject[] sceneryBlockObjects; 

    private PathGenerator pathGenerator;

    void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        List<Vector2Int> pathBlocks = pathGenerator.GeneratePath();

        int pathSize = pathBlocks.Count;
            
        while (pathSize < minPathLength)
        {
            pathBlocks = pathGenerator.GeneratePath();
            while (pathGenerator.GenerateCrossRoads());
            pathSize = pathBlocks.Count;
        }
        StartCoroutine(CreateGrid(pathBlocks));
    }

    IEnumerator CreateGrid(List<Vector2Int> pathBlocks)
    {
        yield return LayPathBlocks(pathBlocks);
        yield return LaySceneryBlocks();
    }

    private IEnumerator LayPathBlocks (List<Vector2Int> pathBlocks)
    {
        foreach (Vector2Int pathBlock in pathBlocks)
        {
            int neighbourValue = pathGenerator.getPathblockRotation(pathBlock.x, pathBlock.y);
            //Debug.Log("Tile" + pathBlock.x + ", " + pathBlock.y + "neighbour value =" + neighbourValue);
            GameObject pathTile = pathBlockObject[neighbourValue].blockPrefab;
            GameObject pathTileBlock = Instantiate(pathTile, new Vector3(pathBlock.x, 0f, pathBlock.y), Quaternion.identity);
            pathTileBlock.transform.Rotate(0f, pathBlockObject[neighbourValue].yRotation, 0f, Space.Self);

            yield return new WaitForSeconds(0.1f);
        }
         yield return null;
    }
    IEnumerator LaySceneryBlocks()
    {
        Debug.Log("lay scenery cells");
        for (int y = gridHeight -1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if(pathGenerator.BlockIsEmpty(x, y))
                {
                    int randomSceneryBlockIndex = Random.Range(0, sceneryBlockObjects.Length);
                    Instantiate(sceneryBlockObjects[randomSceneryBlockIndex].blockPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    
                    yield return new WaitForSeconds(0.025f);
                }
            }
        }
        yield return null;
    }
}
