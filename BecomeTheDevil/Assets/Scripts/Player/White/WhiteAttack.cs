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
    private float gapCnt = 0;
    private float coolCnt = 0;

    //총알
    public float bulletSpeed;
    public float maxSpeed;

    public float AttackCoolTime;
    public float bulletGapTime;

    private float maxDis;
    public float bulletNum;

    private Vector3 bulletPos;

    private float angle;
    private float angleWhite;

    public bool isRuneActive;

    private GameObject magicRune;
    private Animator magicRuneAnimator;

    private GameObject runePosL;
    private GameObject runePosR;

    public float attackTerm;

    private float runeActiveCnt;
    private float shotBulletCnt;

    // Start is called before the first frame update
    void Start()
    {
        magicRune = gameObject.transform.Find("white_magicRune").gameObject;
        magicRuneAnimator = magicRune.GetComponent<Animator>();

        runePosL = gameObject.transform.Find("white_magicRune_point").gameObject.transform.Find("1").gameObject;
        runePosR = gameObject.transform.Find("white_magicRune_point").gameObject.transform.Find("2").gameObject;

        isRuneActive = false;


        bulletSpeed = 5;
        maxSpeed = 30;

        AttackCoolTime = 0.5f;
        bulletGapTime = 0.1f;

        attackTerm = 3f;

        bulletNum = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        //Status 에서 값 받아와야함
        //bulletSpeed = 7f;
        //AttackCoolTime = 0.5f;
        maxDis = 6f;
        //bulletNum = 2f;
        //maxSpeed = 10f;

        trans = GetComponent<Transform>();

        if (!canShoot)
        {
            gapCnt += Time.deltaTime;
            coolCnt += Time.deltaTime;
        }
        else if (isRuneActive) runeActiveCnt += Time.deltaTime;


        if (runeActiveCnt >= attackTerm) {
            MagicRuneOFF();
        }

        if (!continuouFire && coolCnt >= AttackCoolTime)
        {
            coolCnt = 0;
            gapCnt = 0;
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

        bulletPos = magicRune.transform.position;

        if (_Bullet == null) return;
        targetPos = new Vector3(mousePos.x - bulletPos.x, 0, mousePos.z - bulletPos.z);
        angle = Mathf.Atan2(targetPos.x , targetPos.z) * Mathf.Rad2Deg;

        _Bullet.SetActive(true);


        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxSpeed = maxSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;
        _Bullet.transform.position = bulletPos;
        _Bullet.GetComponent<Bullet>().transform.rotation = Quaternion.Euler(0, angle, 0);
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }

    void MouseDown()
    {
        if (canShoot)
        {
            if (isRuneActive)
            {
                StartCoroutine("WhiteBulletAttack");
            }
            else
            {
                runeActiveCnt = 0;

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                angleWhite = Mathf.Atan2(mousePos.x - trans.position.x, mousePos.z - trans.position.z) * Mathf.Rad2Deg;

                if (angleWhite >= 0) magicRune.transform.position = runePosR.transform.position;
                else magicRune.transform.position = runePosL.transform.position;


                MagicRuneON();

            }
        }
    }

    IEnumerator WhiteBulletShoot()
    {
        shotBulletCnt = 0;

        while (shotBulletCnt < bulletNum)
        {
            gapCnt = 0;

            BulletInfoSetting(ObjectManager.Call().GetObject("WhiteBullet"));
            shotBulletCnt++;

            yield return new WaitUntil(() =>gapCnt > bulletGapTime);
        }
    }

    IEnumerator WhiteBulletAttack()
    {
        canShoot = false;
        continuouFire = true;

        while (continuouFire)
        {
            coolCnt = 0;

            if (!Input.GetMouseButton(0)) continuouFire = false;

            StartCoroutine("WhiteBulletShoot");

            yield return new WaitUntil(() => coolCnt > AttackCoolTime);
        }
    }

    void MagicRuneOFF()
    {
        magicRuneAnimator.SetBool("Wait", true);
        magicRuneAnimator.SetBool("RuneActive",false);
    }

    void MagicRuneON()
    {
        magicRuneAnimator.SetBool("RuneActive", true);
        magicRuneAnimator.SetBool("Wait", false);
    }
}
