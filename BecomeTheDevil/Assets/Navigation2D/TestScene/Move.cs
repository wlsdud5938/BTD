using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Move : MonoBehaviour
{
    public Vector3 w;
    NavMeshAgent agent;
    public GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("target");
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0.0f, 0.0f);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //    {
        //        agent.destination = hit.point;
        //    }
        //}
        agent.destination = target.transform.position;
    }


    //public GameObject target;
    //NavMeshAgent2D nav;
    //private void Start()
    //{
    //    nav = GetComponent<NavMeshAgent2D>();
    //    target = GameObject.FindGameObjectWithTag("Finish").gameObject;
    //}
    //void Update()
    //{
    //        nav.destination = target.transform.position;
    //}
}