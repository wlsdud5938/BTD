using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float bulletSpeed; //총알 속도
    public float maxSpeed;  //총알 최대 속도 (가속을 안하면 0)
    public float maxDis;    //총알 사정거리

    private Vector3 startPos;
    private float dis;

    private Animator animator;

    private float acceleration = 0;

    IEnumerator MoveBullet()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        startPos = transform.position;

        while (true)
        {
            dis = Vector3.Distance(startPos, transform.position);

            if (maxSpeed != 0 && maxSpeed>=bulletSpeed) acceleration = (maxSpeed - bulletSpeed) * (bulletSpeed / maxDis);

            if (dis > maxDis) break;
            bulletSpeed += acceleration * Time.fixedDeltaTime;
            transform.position += target.normalized * bulletSpeed * Time.deltaTime;
            yield return null;
            
        }

        animator.SetBool("Boom", true);
    }
}
