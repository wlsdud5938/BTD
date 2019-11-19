using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingRendering : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public float positionOffset = 0.0f;     // 위치 기준점 조정 오프셋


    private void FixedUpdate()
    {
        for(int i = 0; i < sprites.Length; ++i)
            sprites[i].sortingOrder = 10000 - (int)((transform.position.z + positionOffset) * 100) - (int)(transform.position.x + positionOffset);
    }
}
