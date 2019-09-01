using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGem : MonoBehaviour
{
    public RoomInfo background;
    // Start is called before the first frame update
    void Start()
    {
        background = transform.parent.Find("00_Background").GetComponent<RoomInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
