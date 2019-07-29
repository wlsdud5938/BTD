using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;

    public float duration = 20f;  //총알활성 시간
    public float bulletSpeed = 2.0f;

    public Vector3 moveDir;



    IEnumerator MoveBullet()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration) break;

            transform.Translate(target.normalized * Time.deltaTime * bulletSpeed);
            yield return null;

        }

        gameObject.SetActive(false);
    }

    /*** 예전 코드
    public Vector3 target;
    float bulletSpeed = 2.0f;
    public Transform trans;
    public Vector3 startTrans;
    public Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, 5.0f);
        trans = GetComponent<Transform>();
        startTrans = trans.position;
//        target += startTrans;
    }

    // Update is called once per frame
    void Update()
    {
        startTrans = trans.position;
        trans.Translate(moveDir.normalized * bulletSpeed * Time.deltaTime, Space.Self);
    }

    ***/
}
