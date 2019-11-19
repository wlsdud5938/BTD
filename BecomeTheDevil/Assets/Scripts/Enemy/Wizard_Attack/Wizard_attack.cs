using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard_attack : MonoBehaviour
{
    private Animator myAnimator;
    private GameObject root;

    GameObject target;

    bool isStay;
    float aTime;
    float cTime;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
        root = transform.parent.gameObject;
        isStay = root.GetComponent<Wizard>().isStay;
        aTime = root.GetComponent<Wizard>().aTime;
        cTime = root.GetComponent<Wizard>().cTime;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log("inplayer");
            myAnimator.SetBool("Charge", true);
            myAnimator.SetBool("ToNormal", false);
            root.GetComponent<Wizard>().isStay = true;
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

            root.GetComponent<Wizard>().isStay = true;
            Debug.Log("inplayer");

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
            Debug.Log("outplayer");


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