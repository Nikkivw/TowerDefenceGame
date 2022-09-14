using System.Collections;
using System.Collections.Generic;
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
        
        /*
        for (int x = 0; x < width; x++)
        {
            pathBlocks.Add(new Vector2Int(x, y));
        }
        */
        
       
        while (x < width)
        {
            pathBlocks.Add(new Vector2Int(x, y));

            bool validmove = false;

            while (!validmove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0)
                {
                    x++;
                    validmove = true;
                }
                else if (move == 1)
                {
                    y++;
                    validmove=true;
                }
                else if (move == 2)
                {
                    y--;
                    validmove=true;
                }
            }
        }
        
        return pathBlocks;
    }
}