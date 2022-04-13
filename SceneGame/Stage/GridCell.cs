using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [Header("OBJ")]
    [SerializeField] private GameObject srcOBJCell;
    [SerializeField] private Vector3 objSize;
    [Space(15)]
    [Header("Grid")]
    [SerializeField] private float      dim;
    [SerializeField] private Vector2Int gridSize;

    public void GenerateCell()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                GameObject newCell = Instantiate(srcOBJCell);
                newCell.transform.SetParent(this.transform);
                newCell.transform.localScale = objSize;
                newCell.transform.localPosition = new Vector3(
                    i* (objSize.x + dim), 
                    0.1f,
                    j* (objSize.y + dim)
                    );
            }
        }
    }
}
