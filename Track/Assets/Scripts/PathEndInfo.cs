using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEndInfo : MonoBehaviour {
    private SpriteRenderer myRenderer;
    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        int randomIndex = Random.Range(0, Managers.ins.fx.EndPointSprites.Count);
        myRenderer.sprite = Managers.ins.fx.EndPointSprites[randomIndex];

    }
    // Update is called once per frame
    void Update () {
		
	}
}
