using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy
{
    private Animator wizardAnimator;
    NavMeshAgent nav;

    bool continuouFire = false;
    GameObject target;

    GameObject firePos;

    FindAggroTarget aggro;

    public bool canAttack = true;
    public float aTime = 0.0f;
    public float AttackCooltime = 2.0f;

    public bool isStay = false;
    private bool charge = false;
    public float cTime = 0.0f;
    public float ChargeTime = 2.0f;

    float setDis = 0.5f;

    //
    private float bulletSpeed=1;
    
    private float maxDis=6;
    private float maxSpeed=3;

    bool isShoot = false;
    public Vector3 targetPos;
    public Vector3 wandPos;
    GameObject wand;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wizardAnimator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        firePos = transform.GetChild(2).gameObject;
        aggro = GetComponent<FindAggroTarget>();
        wand = transform.GetChild(2).gameObject;
        wandPos = wand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        wandPos = wand.transform.position;
        base.Update();
        target = aggro.target;
        charge = wizardAnimator.GetBool("Charge");
        if (isStay)
        {
            if (charge == true)
            {
                // 차지시간
                cTime += Time.deltaTime;
                wizardAnimator.SetFloat("ChargeTime", cTime);
                BulletFire();
            }
            if (charge == false)
            {
                // 차지 안하고 공격 대기시간.
                aTime += Time.deltaTime;
                wizardAnimator.SetFloat("AttackTime", aTime);
            }
            if (!canAttack)
            {

            }
            if (cTime > ChargeTime)
            {
                // armdown

                wizardAnimator.SetBool("AttackComplete", true);
                wizardAnimator.SetBool("ReturnCharge", false);
                wizardAnimator.SetBool("Charge", false);
                cTime = 0.0f;
                charge = false;
            }
            if (aTime > AttackCooltime)
            {
                // armup

                //wizardAnimator.SetTrigger("ChargeT");
                //wizardAnimator.SetBool("Charge", true);
                wizardAnimator.SetBool("ReturnCharge", true);
                wizardAnimator.SetBool("AttackComplete", false);
                wizardAnimator.SetBool("Charge", true);
                aTime = 0.0f;
                charge = true;
            }
        }
    }

    public void BulletFire()
    {
        if (!isShoot)
        {
            BulletInfoSetting(ObjectManager.Call().GetObject("WizardBullet"));
            isShoot = true;
        }
    }

    void BulletInfoSetting(GameObject _Bullet)
    {
        if (_Bullet == null || target == null) return;
        targetPos = new Vector3(target.transform.position.x - wandPos.x, 0, target.transform.position.z - wandPos.z);

        _Bullet.SetActive(true);

        _Bullet.transform.position = wandPos + targetPos.normalized * setDis;

        _Bullet.GetComponent<WizardBullet>().maxSpeed = maxSpeed;
        _Bullet.GetComponent<WizardBullet>().target = targetPos;
        _Bullet.GetComponent<WizardBullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<WizardBullet>().maxDis = maxDis;
    }
    void ArmDownFinish()
    {
        isShoot = false;
    }
}
