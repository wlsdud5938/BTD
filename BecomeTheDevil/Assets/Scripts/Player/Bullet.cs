using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        moveDir = target - startTrans;
        trans.Translate(moveDir.normalized * bulletSpeed * Time.deltaTime, Space.Self);
    }
}
