using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitInfo : MonoBehaviour {
    public int rowNumber;
    public int columnNumber;
    public bool isPathUnit = false;
    public bool isRock = false;
    public bool isClicked = false;
    [SerializeField]
    public bool isEnd = false;
    private SpriteRenderer myRenderer;
    public Animator myAnimator;
    // Use this for initialization
    public State gridState;
    public GameObject subSprite;
    public enum State
    {
        Empty,
        Rock,
        Path,
        Start,
        End
    }

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        SetState(State.Empty);
    }

	
    public void SetState(State state)
    {
        gridState = state;
        switch (state)
        {
            case State.Empty:
                int randomIndex = Random.Range(0, Managers.ins.fx.GridUnitSprites.Count);
                myRenderer.sprite = Managers.ins.fx.GridUnitSprites[randomIndex];
                break;
            case State.Path:
                subSprite = null;
                break;
            case State.Rock:
                Clicked(false);
                subSprite.GetComponent<SpriteRenderer>().sprite = Managers.ins.fx.RockSprites[Random.Range(0, Managers.ins.fx.RockSprites.Count)];
                break;
            case State.Start:
                Clicked(false);
                subSprite = null;
                break;
            case State.End:
                subSprite = null;
                break;
        }

    }
    public void Clicked(bool playerClicked = true)
    {
        if(isClicked == false)
        {
            isClicked = true;
            //GetComponent<SpriteRenderer>().enabled = false;
            
            myAnimator.SetTrigger("clicked");
            if (playerClicked)
            {
                Managers.ins.gui.IncScore();
            }

            if (gridState == GridUnitInfo.State.End)
            {
                Managers.ins.path.StartCoroutine("RevealPath");
            }
        }
    }
    void OnMouseDown()
    {
        Clicked();
        
    }
}
