using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : MonoBehaviour
{

    public Vector3 target;

    public float bulletSpeed; //총알 속도
    public float maxSpeed;  //총알 최대 속도 (가속을 안하면 0)
    public float maxDis;    //총알 사정거리

    private Vector3 startPos;
    private float dis;

    private Animator animator;

    private float acceleration = 0;

    bool isBoom = false;
    GameObject innerObj;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Charge", true);
        Debug.Log("startBullet");
        innerObj = transform.GetChild(0).gameObject;
    }

    void Go()
    {
        StartCoroutine("MoveBullet");
        animator.SetBool("ChargeDone", true);

    }

    private void Update()
    {
        if(gameObject.activeSelf)
            animator.SetBool("Charge", true);

    }

    IEnumerator MoveBullet()
    {
        animator = GetComponent<Animator>();

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
        if(!isBoom)
        animator.SetBool("Boom", true);
    }

    void ActiveFalse()
    {

        animator.SetBool("Boom", false);
        animator.SetBool("Charge", false);
        animator.SetBool("ChargeDone", false);
        innerObj.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        isBoom = true;
        animator.SetBool("Boom", true);
    }


}
