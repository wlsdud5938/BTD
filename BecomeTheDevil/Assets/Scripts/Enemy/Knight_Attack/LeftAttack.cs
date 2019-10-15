using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAttack : MonoBehaviour
{
    private Animator myAnimator;
    private GameObject root;
    KnightAttack attack;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.transform.parent.GetComponent<Animator>();
        root = transform.parent.transform.parent.gameObject;
        attack = transform.parent.parent.GetComponent<KnightAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log("left");
            //root.transform.localScale = new Vector3(-1, 1, 1);
            //myAnimator.SetTrigger("Attack");
            attack.isTargetIn = true;
            myAnimator.SetBool("Attack", true);
            myAnimator.SetFloat("AttackX", -1.0f);
            myAnimator.SetFloat("AttackY", 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log("left");

            //Debug.Log(root);
            //root.transform.localScale = new Vector3(-1, 1, 1);
            //myAnimator.SetTrigger("Attack");
            //Debug.Log("왼쪽에서 나감");
            myAnimator.SetBool("Attack", false);
            attack.isTargetIn = false;
            //myAnimator.SetFloat("DirX", 0.0f);
            //myAnimator.SetFloat("DirY", 0.0f);
        }
    }
}
