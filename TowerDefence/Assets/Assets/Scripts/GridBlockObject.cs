using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GridBlock", menuName = "TowerDefence/Grid Block")]
public class GridBlockObject : ScriptableObject
{
    public enum BlockType {Path, Ground}

    public BlockType blockType;
    public GameObject blockPrefab;
    public int yRotation;
}
