using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitInfo : MonoBehaviour {
    public int rowNumber;
    public int columnNumber;
    public bool isPathUnit = false;
    public bool isClicked = false;
    private SpriteRenderer myRenderer;
    public Animator myAnimator;
	// Use this for initialization
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
	void Start () {
        myRenderer = GetComponent<SpriteRenderer>();
        
        int randomIndex = Random.Range(0, Managers.ins.fx.GridUnitSprites.Count);
        myRenderer.sprite = Managers.ins.fx.GridUnitSprites[randomIndex];
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MakePathUnit()
    {
        isPathUnit = true;

    }
    public void Clicked()
    {
        if(isClicked == false)
        {
            isClicked = true;
            //GetComponent<SpriteRenderer>().enabled = false;
            
            myAnimator.SetTrigger("clicked");
            Managers.ins.gui.IncScore();

        }
    }
    void OnMouseDown()
    {
        Clicked();
        
    }
}
