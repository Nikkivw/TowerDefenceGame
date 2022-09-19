using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    private List<Vector2Int> pathBlocks;

    public PathGenerator(int width, int height)
    {
        this.width = width; 
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathBlocks = new List<Vector2Int>();
        
        int y = (int)(height / 2);
        
        int x = 0;
        
        while (x < width)
        {
            pathBlocks.Add(new Vector2Int(x, y));

            bool validmove = false;

            while (!validmove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0 || x >(width - 2))
                {
                    x++;
                    validmove = true;
                }
                else if (move == 1 && BlockIsEmpty(x, y + 1) && y < (height - 2))
                {
                    y++;
                    validmove=true;
                }
                else if (move == 2 && BlockIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validmove=true;
                }
            }
        }
        
        return pathBlocks;
    }

    public bool GenerateCrossRoads()
    {
        for (int i = 0; i < pathBlocks.Count; i++)
        {
            Vector2Int pathBlock = pathBlocks[i];

            if (pathBlock.x > 3 && pathBlock.x < width - 4 && pathBlock.y > 2 && pathBlock.y < height - 3)
            {
                if (BlockIsEmpty(pathBlock.x, pathBlock.y + 3) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y + 3) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y + 3) &&
                    BlockIsEmpty(pathBlock.x - 1, pathBlock.y + 2) && BlockIsEmpty(pathBlock.x, pathBlock.y + 2) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y + 2) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y + 2) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y + 2) &&
                    BlockIsEmpty(pathBlock.x - 1, pathBlock.y + 1) && BlockIsEmpty(pathBlock.x, pathBlock.y + 1) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y + 1) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y + 1) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y + 1) &&
                    BlockIsEmpty(pathBlock.x + 1, pathBlock.y) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y) &&
                    BlockIsEmpty(pathBlock.x + 1, pathBlock.y - 1) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y - 1))
                {
                    pathBlocks.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathBlock.x + 1, pathBlock.y), new Vector2Int(pathBlock.x + 2, pathBlock.y), new Vector2Int(pathBlock.x + 2, pathBlock.y + 1), new Vector2Int(pathBlock.x + 2, pathBlock.y + 2), new Vector2Int(pathBlock.x + 1, pathBlock.y + 2), new Vector2Int(pathBlock.x, pathBlock.y + 2), new Vector2Int(pathBlock.x, pathBlock.y + 1) });
                    return true;
                }

                if (BlockIsEmpty(pathBlock.x + 1, pathBlock.y + 1) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y + 1) &&
                    BlockIsEmpty(pathBlock.x + 1, pathBlock.y) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y) &&
                    BlockIsEmpty(pathBlock.x - 1, pathBlock.y - 1) && BlockIsEmpty(pathBlock.x, pathBlock.y - 1) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y - 1) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y - 1) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y - 1) &&
                    BlockIsEmpty(pathBlock.x - 1, pathBlock.y - 2) && BlockIsEmpty(pathBlock.x, pathBlock.y - 2) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y - 2) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y - 2) && BlockIsEmpty(pathBlock.x + 3, pathBlock.y - 2) &&
                    BlockIsEmpty(pathBlock.x, pathBlock.y - 3) && BlockIsEmpty(pathBlock.x + 1, pathBlock.y - 3) && BlockIsEmpty(pathBlock.x + 2, pathBlock.y - 3))
                {
                    pathBlocks.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathBlock.x + 1, pathBlock.y), new Vector2Int(pathBlock.x + 2, pathBlock.y), new Vector2Int(pathBlock.x + 2, pathBlock.y - 1), new Vector2Int(pathBlock.x + 2, pathBlock.y - 2), new Vector2Int(pathBlock.x + 1, pathBlock.y - 2), new Vector2Int(pathBlock.x, pathBlock.y -2), new Vector2Int(pathBlock.x, pathBlock.y - 1)});
                    return true;
                }
            }
        } 
        return false;
    }

    public bool BlockIsEmpty (int x, int y)
    {
        return !pathBlocks.Contains(new Vector2Int(x, y));
    }

    public bool BlockIsTaken(int x, int y)
    {
        return pathBlocks.Contains(new Vector2Int(x, y));
    }

    public int getPathblockRotation(int x, int y)
    {
        int returnValue = 0; 

        if (BlockIsTaken(x, y - 1))
        {
            returnValue += 1;//Check tile down
        }

        if (BlockIsTaken(x - 1, y))
        {
            returnValue += 2;//Check tile left
        }
        
        if (BlockIsTaken(x + 1, y ))//Check tile right
        {
            returnValue += 4;
        }

        if (BlockIsTaken(x, y + 1))//Check tile up
        {
            returnValue += 8;
        }

        return returnValue;
    }
}