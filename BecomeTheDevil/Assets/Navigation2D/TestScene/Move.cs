using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public Vector3 w;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<NavMeshAgent2D>().destination = w;
        }
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