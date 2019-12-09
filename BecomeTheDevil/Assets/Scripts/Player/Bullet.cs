using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float bulletSpeed; //총알 속도
    public float maxSpeed;  //총알 최대 속도 (가속을 안하면 0)
    public float maxDis;    //총알 사정거리
    public float damage;
    public float chargeTime;

    private Vector3 startPos;
    private float dis;

    private Animator animator;

    private float acceleration = 0;

    IEnumerator MoveBullet()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        startPos = transform.position;

        animator.SetBool("Charge", true);

        yield return new WaitForSeconds(chargeTime);
        animator.SetBool("ChargeDone", true);


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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (gameObject.CompareTag("FriendlyBullet"))
        {
            if (other.GetComponent<Health>() != null && (other.CompareTag("Enemy") || other.CompareTag("Field")))
            {
                other.GetComponent<Health>().GetDamage(damage);
                maxDis = 0;
                animator.SetBool("Boom", true);
            }
        }
        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (other.GetComponent<Health>() != null && (other.CompareTag("Unit") || other.CompareTag("Player")))
            {
                other.GetComponent<Health>().GetDamage(damage);
                maxDis = 0;

                animator.SetBool("Boom", true);
            }
        }
    }


}
