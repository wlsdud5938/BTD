using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTest : MonoBehaviour
{

    public GameObject curRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            if (other.name == "00_Background")
                curRoom = other.gameObject;

        }
    }
}
