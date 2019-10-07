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
    public float maxSpeed;

    public float AttackCoolTime;
    public float BulletGapTime;

    private float maxDis;
    private float bulletNum;

    private Vector3 bulletPos;

    private float angle;

    public bool isRuneActive;

    private GameObject magicRune;
    private Animator magicRuneAnimator;

    public float attackTerm;

    private float termCnt;
    private float shotBulletCnt;

    // Start is called before the first frame update
    void Start()
    {
        magicRune = gameObject.transform.Find("white_magicRune").gameObject;
        magicRuneAnimator = magicRune.GetComponent<Animator>();

        isRuneActive = false;


        bulletSpeed = 7;
        maxSpeed = 10;

        AttackCoolTime = 0.5f;
        BulletGapTime = 0.2f;

        attackTerm = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //Status 에서 값 받아와야함
        //bulletSpeed = 7f;
        //AttackCoolTime = 0.5f;
        maxDis = 6f;
        bulletNum = 2f;
        //maxSpeed = 10f;

        trans = GetComponent<Transform>();

        if (!canShoot) cnt += Time.deltaTime;
        else if(isRuneActive) termCnt += Time.deltaTime;


        if (termCnt >= attackTerm) {
            MagicRuneOFF();
        }

        if (!continuouFire && cnt >= AttackCoolTime)
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
                termCnt = 0;
                shotBulletCnt = 0;
                MagicRuneON();
            }
        }
    }

    IEnumerator WhiteBulletAttack()
    {
        canShoot = false;
        continuouFire = true;
        while (continuouFire)
        {
            cnt = 0;
            termCnt = 0;

            BulletInfoSetting(ObjectManager.Call().GetObject("WhiteBullet"));

            if (!Input.GetMouseButton(0)) continuouFire = false;
            shotBulletCnt++;
            if (shotBulletCnt % bulletNum != 0) yield return new WaitUntil(() => cnt > BulletGapTime);
            else
                yield return new WaitUntil(() => cnt > AttackCoolTime);
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
