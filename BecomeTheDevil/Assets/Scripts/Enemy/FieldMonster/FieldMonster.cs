using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMonster : MonoBehaviour
{
    public RoomInfo roominfo;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void die()
    {
        roominfo.countFieldUnit--;
        Destroy(gameObject);
    }
}
