using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public List<GameObject> unitList;
    public int maxUnit = 5;
    public int curUnitCount = 0;
    public bool isClosed = false;
    public float time = 0.0f;
    public int countFieldUnit = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClosed)
        {
            time += Time.deltaTime;
        }
        if(countFieldUnit>0)
        {
            isClosed = true;
        }
    }
}
