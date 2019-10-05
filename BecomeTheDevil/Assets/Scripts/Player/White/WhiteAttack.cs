using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteAttack : MonoBehaviour
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
    public float bulletSpeed = 7;
    public float maxSpeed = 15;

    private float AttackCooltime;
    private float maxDis;
    private float bulletNum;

    private Vector3 bulletPos;

    private float angle;

    private Transform b1;
    private Transform b2;
    private Transform b3;
    private Transform b4;
    private Transform b5;

    private Transform[] bulletStartPos;


    // Start is called before the first frame update
    void Start()
    {
        b1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform;
        b2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform;
        b3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform;
        b4 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("4").transform;
        b5 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("5").transform;

        bulletStartPos = new Transform[5] { b1, b2, b3, b4, b5 };
    }

    // Update is called once per frame
    void Update()
    {
        //Status 에서 값 받아와야함
        //bulletSpeed = 7f;
        AttackCooltime = 0.5f;
        maxDis = 6f;
        bulletNum = 2f;
        //maxSpeed = 10f;

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

    void BulletInfoSetting(GameObject _Bullet, int posNum)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (_Bullet == null) return;

        _Bullet.SetActive(true);


        if (posNum > 5 || posNum < 1)
        {
            return;
        }
        else
        {
            bulletPos = bulletStartPos[posNum - 1].position;
        }


        targetPos = new Vector3(mousePos.x - bulletPos.x, 0, mousePos.z - bulletPos.z);

        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;

        angle = Vector3.SignedAngle(transform.up, targetPos - bulletPos, -transform.forward);

        _Bullet.transform.position = trans.position;

        _Bullet.GetComponent<WhiteBullet>().angle = angle;
        _Bullet.GetComponent<WhiteBullet>().bulletPos = bulletPos;
        _Bullet.GetComponent<WhiteBullet>().posNum= posNum;
        _Bullet.GetComponent<WhiteBullet>().StartCoroutine("LoadBullet");
    }

    void MouseDown()
    {
        if (canShoot)
            StartCoroutine("WhiteBulletAttack");
    }

    IEnumerator WhiteBulletAttack()
    {
        canShoot = false;
        continuouFire = true;
        while (continuouFire)
        {
            cnt = 0;

            BulletInfoSetting(ObjectManager.Call().GetObject("WhiteBullet"), 2);
            BulletInfoSetting(ObjectManager.Call().GetObject("WhiteBullet"), 4);

            yield return new WaitUntil(() => cnt > AttackCooltime);
        }
    }
}
