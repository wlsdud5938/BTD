using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float duration;  //총알활성 시간
    public float bulletSpeed; //총알 속도
    public float maxDis;    //총알 사정거리

    public Vector3 moveDir;

    private Vector3 startPos;
    private float dis;

    IEnumerator MoveBullet()
    {
        startPos = transform.position;

        while (true)
        {
            dis = Vector3.Distance(startPos, transform.position);
            if (dis > maxDis) break;

            transform.Translate(target.normalized * Time.deltaTime * bulletSpeed);
            yield return null;

        }

        gameObject.SetActive(false);
    }
}
