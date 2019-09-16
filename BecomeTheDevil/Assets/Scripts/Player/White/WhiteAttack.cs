using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

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
    public float bulletSpeed;
    public float AttackCooltime;
    public float maxDis;
    public float bulletNum;

    public float loadBullet = 1f;
    public float waitBullet = 1f;

    Vector3 whiteBulletPoint_1;
    Vector3 whiteBulletPoint_2;
    Vector3 whiteBulletPoint_3;
    Vector3 whiteBulletPoint_4;
    Vector3 whiteBulletPoint_5;

    Vector3 bulletPos;

    private float angle;

    private Transform b1;
    private Transform b2;
    private Transform b3;
    private Transform b4;
    private Transform b5;


    // Start is called before the first frame update
    void Start()
    {
        b1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform;
        b2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform;
        b3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform;
        b4 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("4").transform;
        b5 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("5").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Status 에서 값 받아와야함
        bulletSpeed = 1f;
        AttackCooltime = 0.5f;
        maxDis = 6f;
        bulletNum = 2f;

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


        if (posNum == 1)
        {
            bulletPos = b1.position;
        }
        else if (posNum == 2)
        {
            bulletPos = b2.position;
        }
        else if (posNum == 3)
        {
            bulletPos = b3.position;
        }
        else if(posNum == 4)
        {
            bulletPos = b4.position;
        }
        else if (posNum == 5)
        {
            bulletPos = b5.position;
        }
        else
        {
            return;
        }

        targetPos = new Vector3(mousePos.x - bulletPos.x, 0, mousePos.z - bulletPos.z);

        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;

        angle = Vector3.SignedAngle(transform.up, bulletPos - targetPos, -transform.forward);
        _Bullet.transform.position = transform.position;

        Sequence seq = DOTween.Sequence();
        seq.Append(_Bullet.transform.DOMove(bulletPos, loadBullet));
        seq.Join(_Bullet.transform.DORotate(new Vector3(0, angle, 0), loadBullet));
        seq.AppendInterval(waitBullet);
 
        seq.Play();


        _Bullet.GetComponent<Bullet>().transform.GetChild(0).gameObject.GetComponent<Animator>().Play("white_bullet_basic_shoot");
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
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
