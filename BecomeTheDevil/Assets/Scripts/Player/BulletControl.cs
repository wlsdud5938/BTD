using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    GameObject _Bullet;

    void Start()
    {
        _Bullet = gameObject.transform.parent.gameObject;
    }

    public void SetActiveOff()
    {
        _Bullet.SetActive(false);
    }

    public void MoveBullet()
    {
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }
}
