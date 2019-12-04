using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeAnimation : MonoBehaviour
{
    public SpriteRenderer targetSprite;
    public Sprite[] sprites;

    public void ChangeSprite(int index)
    {
        targetSprite.sprite = sprites[index];
    }
}
