using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingRendering : MonoBehaviour
{
    
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sortingOrder = 1000000 - (int)(transform.position.z*500);
    }
}
