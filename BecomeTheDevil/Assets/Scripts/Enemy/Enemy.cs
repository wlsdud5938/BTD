using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Vector3 curLocation;
    protected Vector3 preLocation;

    protected Animator enemyAnimator;

    // Start is called before the first frame update
    protected void Start()
    {
        preLocation = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        checkLR();
    }

    void checkLR()
    {
        curLocation = transform.position;
        if (preLocation.x < curLocation.x)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            enemyAnimator.SetFloat("DirX", 1.0f);
        }
        else if (preLocation.x > curLocation.x)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            enemyAnimator.SetFloat("DirX", -1.0f);
        }
        else if (preLocation.x == curLocation.x && preLocation.z == curLocation.z)
        {
            enemyAnimator.SetBool("Walking", false);
        }
        preLocation = transform.position;
    }

}
