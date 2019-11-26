using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTarget : MonoBehaviour
{
    FindAggroTarget aggroTarget;
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = transform.root.GetComponent<FindAggroTarget>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            aggroTarget.AddList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            aggroTarget.RemoveList(other.gameObject);
        }
    }
    
}
