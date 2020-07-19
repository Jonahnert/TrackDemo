using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    public List<Sprite> SpriteList;
    // Use this for initialization
    public void SetSprite()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        int randomIndex = Random.Range(0, SpriteList.Count);
        myRenderer.sprite = SpriteList[randomIndex];

    }

    public void SetSpriteList(List<Sprite> newList)
    {
        SpriteList = newList;
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
