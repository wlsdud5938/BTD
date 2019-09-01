using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    protected Vector3 curLocation;
    protected Vector3 preLocation;

    protected Animator enemyAnimator;

    //private bool wasRight = false;
    private bool wasLeft = false;

    // Start is called before the first frame update
    protected void Start()
    {
        preLocation = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        checkDir();
    }

    void checkDir()
    {
        curLocation = transform.position;
        
        if (preLocation.x < curLocation.x)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            enemyAnimator.SetFloat("DirX", 1.0f);
            wasLeft = false;
            //wasRight = true;
        }
        else if (preLocation.x > curLocation.x)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            enemyAnimator.SetFloat("DirX", -1.0f);
            wasLeft = true;
        }

        if (preLocation.z < curLocation.z && preLocation.x == curLocation.x)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            if (wasLeft)
            {
                enemyAnimator.SetFloat("DirX", 0.0f);
                enemyAnimator.SetFloat("DirY", -1.0f);
            }
            else
            {
                enemyAnimator.SetFloat("DirX", 0.0f);
                enemyAnimator.SetFloat("DirY", 1.0f);
            }
            
        }
        else if (preLocation.z > curLocation.z && preLocation.x == curLocation.x)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            enemyAnimator.SetBool("Walking", true);
            if (wasLeft)
            {
                enemyAnimator.SetFloat("DirX", -1.0f);
                enemyAnimator.SetFloat("DirY", 0.0f);
            }
            else
            {
                enemyAnimator.SetFloat("DirX",  1.0f);
                enemyAnimator.SetFloat("DirY", 0.0f);
            }
        }

        else if (preLocation.x == curLocation.x && preLocation.z == curLocation.z)
        {
            enemyAnimator.SetBool("Walking", false);
            if (wasLeft)
            {
                enemyAnimator.SetFloat("DirX", -1.0f);
                enemyAnimator.SetFloat("DirY", 0.0f);
            }
            else
            {
                enemyAnimator.SetFloat("DirX", 1.0f);
                enemyAnimator.SetFloat("DirY", 0.0f);
            }
        }

        preLocation = transform.position;
    }
    
}
