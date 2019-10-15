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
    public Status unitTarget;    // 사거리 안에 들어온 target
    Status stat;
    private Queue<Status> unit = new Queue<Status>();   // target queue
    bool isSearch = false;
    private Animator myAnimator;
    public bool isTargetIn = false;
    // Start is called before the first frame update
    void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
        myAnimator = transform.GetComponent<Animator>();
        stat = transform.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTargetIn = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTargetIn = false;
            target = null;
        }
    }

    void TakeDamage()
    {
        Debug.Log("player");

        if (!isTargetIn)
            return;
        if(target)
            target.GetComponent<Status>().curHP -= stat.attackDMG;
    }
    void Stop()
    {
        Debug.Log("stop");
        nav.speed = 0;
    }
    void Go()
    {
        Debug.Log("go");
        if (!isTargetIn)
        {
            nav.speed = 3.5f;

        }

    }
}
