using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard_attack : MonoBehaviour
{
    private Animator myAnimator;
    private GameObject root;
    NavMeshAgent nav;

    public bool canAttack = true;
    private float aTime = 0.0f;
    public float AttackCooltime = 2.0f;

    public bool isStay = false;
    private bool charge = false;
    private float cTime = 0.0f;
    public float ChargeTime = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
        root = transform.parent.gameObject;
        nav = transform.parent.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        charge = myAnimator.GetBool("Charge");
        if (isStay)
        {
            if (charge == true)
            {
                // 차지시간
                cTime += Time.deltaTime;
                myAnimator.SetFloat("ChargeTime", cTime);
            }
            if (charge == false)
            {
                // 차지 안하고 공격 대기시간.
                aTime += Time.deltaTime;
                myAnimator.SetFloat("AttackTime", aTime);
            }
            if (!canAttack)
            {

            }
            if (cTime > ChargeTime)
            {
                // armdown

                myAnimator.SetBool("AttackComplete", true);
                myAnimator.SetBool("ReturnCharge", false);
                myAnimator.SetBool("Charge", false);
                cTime = 0.0f;
                charge = false;
            }
            if (aTime > AttackCooltime)
            {
                // armup

                //myAnimator.SetTrigger("ChargeT");
                //myAnimator.SetBool("Charge", true);
                myAnimator.SetBool("ReturnCharge", true);
                myAnimator.SetBool("AttackComplete", false);
                myAnimator.SetBool("Charge", true);
                aTime = 0.0f;
                charge = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log(root);
            myAnimator.SetBool("Charge", true);
            myAnimator.SetBool("ToNormal", false);
            isStay = true;
            //myAnimator.SetTrigger("Attack");
            //myAnimator.SetTrigger("Charge");
            //myAnimator.SetBool("ToNormal", false);
            //myAnimator.SetBool("AttackComplete", true);
            //StartCoroutine(WaitForIt());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            //myAnimator.SetTrigger("Charge");
            //if (canAttack)
            //{
                //StartCoroutine("Attack");
            //}
            //myAnimator.SetBool("AttackComplete", true);
            //StartCoroutine(WaitForIt());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            //charge = false;
            isStay = false;
            aTime = 0.0f;
            cTime = 0.0f;
            myAnimator.SetBool("Charge", false);
            myAnimator.SetBool("ToNormal", true);
            myAnimator.SetBool("AttackComplete", false);
        }
    }

    //IEnumerator Attack()
    //{
        //canAttack = false;
        //yield return new WaitForSeconds(2.0f);
        //myAnimator.SetBool("AttackComplete", true);
    //}

    //IEnumerator Charge()
    //{
        //canAttack = false;
        //yield return new WaitUntil(() =>  > AttackCooltime);
        //myAnimator.SetBool("Charge", true);
        //yield return new WaitForSeconds(2.0f);
        //myAnimator.SetBool("AttackComplete", true);
    //}

}