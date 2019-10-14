using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private GameObject _Bullet;
    private Animator animator;

    void Start()
    {
        _Bullet = gameObject.transform.parent.gameObject;
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetActiveOff()
    {
        animator.SetBool("isBulletActive", true);
        _Bullet.SetActive(false);
    }

    public void MoveBullet()
    {
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }

    public void BulletActiveOFF()
    {
        animator.SetBool("isBulletActive", false);
    }

    public void IsShoot(){ Debug.Log("Shoot"); }
    public void IsBoom(){ Debug.Log("Boom"); }
    public void IsLoad(){ Debug.Log("Load"); }
}
