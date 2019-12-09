using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public NavMeshAgent nav;
    private bool canAttack = true;
    public float attackTimer = 0.1f;
    public GameObject target;
    bool isSearch = false;
    private Animator myAnimator;
    public bool isTargetIn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.GetComponent<Animator>();
        nav = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Stop()
    {
        nav.speed = 0;
    }
    void Go()
    {
        if (!isTargetIn)
        {
            nav.speed = 3.5f;
            myAnimator.SetBool("Walking", true);
        }

    }
    void TakeDamage()
    {

    }
}
