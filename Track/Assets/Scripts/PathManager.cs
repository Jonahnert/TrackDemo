using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {
    public List<List<GameObject>> listOfRows = new List<List<GameObject>>();
    public List<GameObject> pathUnits = new List<GameObject>();
    private List<GameObject> tempRow = new List<GameObject>();
    private List<GameObject> tempAdjUnits = new List<GameObject>();
    private GameObject startUnit;
    private int pathLength = 0;
    private int pathThreshold = 3;
    private int pathAttempts = 0;
    private int pathAttemptsThresh = 200;

    public int numOfRocks = 0;

    //for debugging
    private int maxPathLength = 0;
    // Use this for initialization
    void Start () {
        CreateRocks();
        CreateStart();
        CreatePath(startUnit);

    }
	
	// Update is called once per frame
    /*
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            CreateStart();
            CreatePath(startUnit);
            //Debug.Log("Count: " + listOfRows.Count);
            for(int x = 0; x < listOfRows.Count; x++)
            {
                List<GameObject> tempList = new List<GameObject>();
                tempList = listOfRows[x];
                //Debug.Log(x + " Count: " + tempList.Count);
                for (int y =0; y < listOfRows[x].Count; y++)
                {
                    //Debug.Log("Row: " + listOfRows[x]);
                    //Debug.Log("Column: " + tempList[y]);
                }
            }
            
        }
		
	}
    */
    public void CreateRocks()
    {
        for (int i = 0; i < numOfRocks; i++)
        {
            int row = Random.Range(0, listOfRows.Count);
            int column = Random.Range(0, listOfRows[row].Count);

            GridUnitInfo gridUnit = listOfRows[row][column].GetComponent<GridUnitInfo>();           
            gridUnit.Clicked(false);
            gridUnit.SetState(GridUnitInfo.State.Rock);

        }
    }  
    public void CreatePath(GameObject previousUnit)
    {
        
        FindAdj(previousUnit);
        tempAdjUnits = FilterUnpathedUnits(tempAdjUnits);
        

        //For Debugging
        maxPathLength += 1;
        if(maxPathLength > 300)
        {
            Debug.Log("AAAAHHH so many recursions");
            return;
        }

        if(tempAdjUnits.Count > 0)
        {
            int adjIndex = Random.Range(0, tempAdjUnits.Count);
            GameObject nextUnit = tempAdjUnits[adjIndex];

            pathUnits.Add(nextUnit);
            pathLength += 1;

            tempAdjUnits.Clear();
            nextUnit.GetComponent<GridUnitInfo>().gridState = GridUnitInfo.State.Path;
            CreatePath(nextUnit);
            return;
        }
        //When there are no valid adj units
        else
        {
            //if path is long enough finish the path
            if(pathLength > pathThreshold)
            {
                Debug.Log("Path Found, it's " + pathLength + " spaces long");
                previousUnit.GetComponent<GridUnitInfo>().SetState(GridUnitInfo.State.End);
                Managers.ins.fx.DrawPath(pathUnits);
                return;
            }

            //if it's tried too many times, error out
            pathAttempts += 1;
            if (pathAttempts >= pathAttemptsThresh)
            {
                Debug.Log("TO MANY PATH ATTEMPTS ERROR");
                return;
            }
            //if it's not clear lists and retry
            pathLength = 0;
            pathUnits.Clear();
            CreateStart();
            CreatePath(startUnit);
            CreateRocks();

        }              

    }

    private void CreateStart()
    {
        //Find a random starting unit
        int startRow = Random.Range(0, listOfRows.Count);
        tempRow = listOfRows[startRow];
        int startColumn = Random.Range(0, tempRow.Count);
        startUnit = tempRow[startColumn];
        if (startUnit.GetComponent<GridUnitInfo>().gridState == GridUnitInfo.State.Empty)
        {
            startUnit.GetComponent<GridUnitInfo>().Clicked();
            startUnit.GetComponent<GridUnitInfo>().SetState(GridUnitInfo.State.Start);
            pathUnits.Clear();
            pathUnits.Add(startUnit);
            pathLength += 1;
            pathAttemptsThresh = 0;
        }
        else
        {
            pathAttempts++;
           if(pathAttempts > pathAttemptsThresh)
            {
                Debug.LogError("too many recursions!");
            }
            else
            {
                CreateStart();
            }

        }
        
    }
    private void FindAdj(GameObject unit)
    {
        tempAdjUnits.Clear();

        int myRow = unit.GetComponent<GridUnitInfo>().rowNumber;
        int myColumn = unit.GetComponent<GridUnitInfo>().columnNumber;

        GameObject unitToAdd = new GameObject();

        //if not at the end of a row get adj (CHECK NORTH) (+1 offsets list starting at 0)
        if ((myRow+1) < Managers.ins.grid.rows)
        {
            List<GameObject> tempList = new List<GameObject>();
            tempList = listOfRows[myRow+1];
            unitToAdd = tempList[myColumn];
            tempAdjUnits.Add(unitToAdd);
            Debug.Log("NORTH");
        }

        //if not at the beginning of a row get adj (CHECK SOUTH)
        if (myRow > 0)
        {
            List<GameObject> tempList = new List<GameObject>();
            tempList = listOfRows[myRow - 1];
            unitToAdd = tempList[myColumn];
            tempAdjUnits.Add(unitToAdd);
            Debug.Log("SOUTH");
        }

        //if not at the end of a column get adj (+1 offsets list starting at 0)
        if ((myColumn+1) < Managers.ins.grid.columns)
        {
            List<GameObject> tempList = new List<GameObject>();
            tempList = listOfRows[myRow];
            unitToAdd = tempList[myColumn+1];
            tempAdjUnits.Add(unitToAdd);
            Debug.Log("EAST");
        }

        //if not at the beginning of a column get adj
        if (myColumn > 0)
        {
            List<GameObject> tempList = new List<GameObject>();
            tempList = listOfRows[myRow];
            unitToAdd = tempList[myColumn - 1];
            tempAdjUnits.Add(unitToAdd);
            Debug.Log("WEST");

        }
        Debug.Log("MY NEIGHBORS: ");
        foreach(GameObject neighbor in tempAdjUnits)
        {
            Debug.Log(neighbor.name);
        }

    }

    private List<GameObject> FilterUnpathedUnits(List<GameObject> AvailableUnits)
    {
        List<GameObject> tempUnitsToRemove = new List<GameObject>();
        tempUnitsToRemove.Clear();
        //takes list AvailableUnits and removes all pathed units
        foreach(GameObject unit in AvailableUnits)
        {
            GridUnitInfo unitInfo = unit.GetComponent<GridUnitInfo>();
            if (unitInfo.gridState != GridUnitInfo.State.Empty)
            {
                tempUnitsToRemove.Add(unit);
            }
        }
        foreach(GameObject unitToRemove in tempUnitsToRemove)
        {
            AvailableUnits.Remove(unitToRemove);
        }
        return AvailableUnits;
    }

    public IEnumerator RevealPath()
    {
        foreach (GameObject unit in pathUnits)
        {
            unit.GetComponent<GridUnitInfo>().Clicked(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
