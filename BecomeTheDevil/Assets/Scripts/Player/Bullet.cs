using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float bulletSpeed; //총알 속도
    public float maxSpeed;  //총알 최대 속도 (가속을 안하면 0)
    public float maxDis;    //총알 사정거리

    public float boomTiming = 1.2f; //터지는 위치

    private Vector3 startPos;
    private float dis;

    private Animator animator;

    private float acceleration;

    IEnumerator MoveBullet()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        startPos = transform.position;

        acceleration = (maxSpeed - bulletSpeed) * (maxSpeed + bulletSpeed) / (2 * maxDis);


        while (true)
        {
            if (maxSpeed != 0 && maxSpeed >= bulletSpeed) bulletSpeed += acceleration * Time.deltaTime;
            dis = Vector3.Distance(startPos, transform.position);
            
            if(dis > maxDis - boomTiming)
            {
                animator.SetBool("Boom", true);
            }
            if (dis > maxDis) break;

            transform.position += target.normalized * bulletSpeed * Time.deltaTime;
            yield return null;
            
        }

        gameObject.SetActive(false);
    }
}
