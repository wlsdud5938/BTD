using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<NavMeshAgent2D>().destination = w;
        }
    }
    /*
     
    Vector3 target;
    GameObject start;
    private void Start()
    {
        start = GameObject.FindGameObjectWithTag("Finish").gameObject;
        target = start.transform.position;
    }
    void Update()
    {
            GetComponent<NavMeshAgent2D>().destination = target;
    }
     */
}