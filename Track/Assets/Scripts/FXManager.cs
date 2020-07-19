using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {
    public GameObject path;
    public GameObject endPointFX;
    public GameObject startPointFX;
    public GameObject rockFX;
    public List<Sprite> RockSprites;
    public List<Sprite> GridUnitSprites;
    public List<Sprite> EndPointSprites;
    // Use this for initialization

    public void DrawPath(List<GameObject> anchorPoints)
    {
        LineRenderer lr = path.GetComponent<LineRenderer>();
        lr.positionCount = anchorPoints.Count;

        //create path
        for (int x = 0; x < anchorPoints.Count; x++)
        {
            lr.SetPosition(x, anchorPoints[x].transform.position);
        }

        //create end point (-1 accounts for list starting at 0)
        Instantiate(endPointFX, anchorPoints[anchorPoints.Count-1].transform.position, Quaternion.identity);

        //create start point
        GameObject.Instantiate(startPointFX, anchorPoints[0].transform.position, Quaternion.identity);
    }

}
