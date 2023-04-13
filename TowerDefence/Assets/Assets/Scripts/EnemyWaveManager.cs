using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject Start_Path;
    private List<Vector2Int> pathBlocks;
    private GameObject enemyInstance;
    int nextPathblockIndex;
    void Start()
    {
        enemyInstance = Instantiate(enemyObject, new Vector3(-1f, -0.5f, 5f), Quaternion.identity);
        nextPathblockIndex = 1;
    }

   
    void Update()
    {
        if (pathBlocks != null && pathBlocks.Count > 1)
        {
            Vector3 currentPos = enemyInstance.transform.position;
            Vector3 nextPos = new Vector3(pathBlocks[nextPathblockIndex].x, 0.4f, pathBlocks[nextPathblockIndex].y);
            enemyInstance.transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime);
            if (Vector3.Distance(currentPos,nextPos)< 0.05f)
            {
                nextPathblockIndex++;
            }
        }
    }
     public void setPathBlocks(List<Vector2Int> pathBlocks)
    {
        this.pathBlocks = pathBlocks;
    }
}
