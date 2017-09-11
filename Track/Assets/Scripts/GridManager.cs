using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public int rows;
    public int columns;
    public GameObject blockPrefab;
    private float Xoffset;
    private float Yoffset;
    private Vector3 startPosition;
    private int rowCounter;
    private int columnCounter;
    private GameObject currentGridUnit;
    private List<GameObject> tempColumn;
    private int unitCounter = 0;

	// Use this for initialization
	void Start () {
        float xSize = blockPrefab.GetComponent<BoxCollider2D>().size.x;
        float ySize = blockPrefab.GetComponent<BoxCollider2D>().size.y;
        Xoffset = blockPrefab.transform.lossyScale.x * xSize;
        Yoffset = blockPrefab.transform.lossyScale.y * ySize;
        startPosition = transform.position;
        CreateGrid();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void CreateGrid()
    {
        
        for (int x = 0; x < rows; x++)
        {
            tempColumn = new List<GameObject>();
            for (int y = 0; y < columns; y++)
            {               
                currentGridUnit = Instantiate(blockPrefab, startPosition, Quaternion.identity);
                currentGridUnit.name = currentGridUnit.name + unitCounter;
                unitCounter += 1;
                startPosition = new Vector3(startPosition.x += Xoffset, startPosition.y, startPosition.z);

                currentGridUnit.GetComponent<GridUnitInfo>().rowNumber = rowCounter;
                currentGridUnit.GetComponent<GridUnitInfo>().columnNumber = columnCounter;
                columnCounter += 1;
                tempColumn.Add(currentGridUnit);
            }
            Managers.ins.path.listOfRows.Add(tempColumn);
            columnCounter = 0;
            rowCounter += 1;
            startPosition = new Vector3(transform.position.x, startPosition.y += Yoffset, startPosition.z);
        }
    }
}
