using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float bulletSpeed; //총알 속도
    public float maxDis;    //총알 사정거리

    private Vector3 startPos;
    private float dis;

    private Animator animatorBullet;

    IEnumerator MoveBullet()
    {
        animatorBullet = GetComponent<Animator>();
        animatorBullet.SetBool("Boom", false);
        startPos = transform.position;

        while (true)
        {
            dis = Vector3.Distance(startPos, transform.position);
            if (dis > maxDis) break;
            if(dis >= maxDis - 3) animatorBullet.SetBool("Boom", true);
            transform.position += target.normalized * bulletSpeed * Time.deltaTime;
            yield return null;
            
        }

        gameObject.SetActive(false);
    }
}
