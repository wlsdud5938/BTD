using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 curLocation;
    public Vector2 preLocation;

    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        preLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
