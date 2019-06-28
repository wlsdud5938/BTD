using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    //void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        GetComponent<NavMeshAgent2D>().destination = w;
    //    }
    //}     
    public Vector3 target;
    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Finish").gameObject.transform.position;
    }
    void Update()
    {
            GetComponent<NavMeshAgent2D>().destination = target;
    }
}