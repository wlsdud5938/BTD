using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRune : MonoBehaviour
{

    GameObject white;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        white = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootBullet()
    {
        white.GetComponent<WhiteAttack>().StartCoroutine("WhiteBulletAttack");
    }

    public void SetON()
    {
        white.GetComponent<WhiteAttack>().isRuneActive = true;
    }

    public void SetOFF()
    {
        white.GetComponent<WhiteAttack>().isRuneActive = false;

    }
}
