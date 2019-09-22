using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_attack : MonoBehaviour
{
    private Animator myAnimator;
    private GameObject root;
    private bool charge;
    private float DelayTime;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
        root = transform.parent.gameObject;
        DelayTime = 0.0f;
    }

    void Update()
    {
        charge = myAnimator.GetBool("Charge");
        if(charge == true)
        {
            DelayTime += Time.deltaTime;
            myAnimator.SetFloat("DelayTime", DelayTime);
            if(DelayTime > 2.0f)
            {
                //myAnimator.SetBool("AttackComplete", true);
                DelayTime = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log(root);
            //myAnimator.SetTrigger("Attack");
            //myAnimator.SetTrigger("Charge");
            myAnimator.SetBool("ToNormal", false);
            myAnimator.SetBool("AttackComplete", true);
            //StartCoroutine(WaitForIt());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            //myAnimator.SetTrigger("Charge");
            myAnimator.SetBool("Charge", true);
            //myAnimator.SetBool("AttackComplete", true);
            //StartCoroutine(WaitForIt());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            myAnimator.SetBool("Charge", false);
            myAnimator.SetBool("AttackComplete", false);
            myAnimator.SetBool("ToNormal", true);
            myAnimator.SetFloat("DelayTime", 0.0f);
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2.0f);
        myAnimator.SetBool("AttackComplete", true);
    }
}