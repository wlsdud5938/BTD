using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
    private Transform trans;
    private Vector3 MovePos = Vector3.zero;

    // 마우스 위치
    private Vector3 mousePos;
    private Vector3 targetPos;


    private bool continuouFire = false;
    private bool canShoot = true;
    private float cnt = 0;

    //총알
    private float bulletSpeed;
    private float AttackCooltime;
    private float maxDis;
    private float maxSpeed;

    public float setDis = 0.5f;


    // Update is called once per frame
    void Update()
    {
        //Status 에서 값 받아와야함
        bulletSpeed = 6f;
        AttackCooltime = 0.5f;
        maxDis = 6f;
        maxSpeed = 0f;

        trans = GetComponent<Transform>();

        if (!canShoot) cnt += Time.deltaTime;
        if (!continuouFire && cnt >= AttackCooltime)
        {
            cnt = 0;
            canShoot = true;
        }

        KeyCheck();
    }

    // 입력키 확인
    void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();
        else if (Input.GetMouseButtonUp(0))
            continuouFire = false;
    }

    void BulletInfoSetting(GameObject _Bullet)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (_Bullet == null) return;
        targetPos = new Vector3(mousePos.x - trans.position.x, 0, mousePos.z - trans.position.z);

        _Bullet.SetActive(true);

        _Bullet.transform.position = trans.position + targetPos.normalized * setDis;

        _Bullet.GetComponent<Bullet>().maxSpeed = maxSpeed;
        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }

    void MouseDown()
    {
        if(canShoot)
            StartCoroutine("GreenBulletAttack");
    }

    IEnumerator GreenBulletAttack()
    {
        canShoot = false;
        continuouFire =true;
        while (continuouFire)
        {
            cnt = 0;
            BulletInfoSetting(ObjectManager.Call().GetObject("GreenBullet"));
            yield return new WaitUntil(() => cnt > AttackCooltime);
        }
    }



}
