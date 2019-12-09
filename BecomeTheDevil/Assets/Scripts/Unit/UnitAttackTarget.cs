using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTarget : MonoBehaviour
{
    public FindAggroTarget aggroTarget;
    public List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = transform.parent.GetComponent<FindAggroTarget>();
    }

    // Update is called once per frame

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
