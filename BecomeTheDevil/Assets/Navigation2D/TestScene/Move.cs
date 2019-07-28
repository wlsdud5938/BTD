using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Move : MonoBehaviour
{
    public Vector3 w;
    NavMeshAgent agent;
    public GameObject target;
    public GameObject core;
    EnemyAggroAI aggAI;
    float findTime = 0;
    public bool aaa = true;
    public bool bbb = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("target");
        aggAI = gameObject.transform.GetChild(0).GetComponent<EnemyAggroAI>();
        core = GameObject.FindGameObjectWithTag("Finish").gameObject;
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0.0f, 0.0f);
        NavMeshPath path = new NavMeshPath();
        agent.destination = target.transform.position;
        bbb = agent.CalculatePath(core.transform.position, path);

        if (target == null || target.activeSelf == false)
            agent.destination = core.transform.position;

        if (path.status == NavMeshPathStatus.PathPartial)
        {
            aggAI.corePath = false;
            Debug.Log("1");
        }
        else
        {
            aggAI.corePath = true;
            Debug.Log("2");

        }
        aaa = aggAI.corePath;

    }

}