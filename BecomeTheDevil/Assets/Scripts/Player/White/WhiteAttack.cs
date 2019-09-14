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
    public float bulletSpeed;
    public float AttackCooltime;
    public float maxDis;
    public float bulletNum;

    Vector3 whiteBulletPoint_1;
    Vector3 whiteBulletPoint_2;
    Vector3 whiteBulletPoint_3;
    Vector3 whiteBulletPoint_4;
    Vector3 whiteBulletPoint_5;


    // Start is called before the first frame update
    void Start()
    {

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
            whiteBulletPoint_1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform.position;
            _Bullet.transform.position = whiteBulletPoint_1;
        }
        else if (posNum == 2)
        {
            whiteBulletPoint_2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform.position;
            _Bullet.transform.position = whiteBulletPoint_2;
        }
        else if (posNum == 3)
        {
            whiteBulletPoint_3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform.position;
            _Bullet.transform.position = whiteBulletPoint_3;
        }
        else if(posNum == 4)
        {
            whiteBulletPoint_4 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("4").transform.position;
            _Bullet.transform.position = whiteBulletPoint_4;
        }
        else if (posNum == 5)
        {
            whiteBulletPoint_5 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("5").transform.position;
            _Bullet.transform.position = whiteBulletPoint_5;
        }
        else
        {
            return;
        }

        targetPos = new Vector3(mousePos.x - _Bullet.transform.position.x, 0, mousePos.z - _Bullet.transform.position.z);

        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;
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
