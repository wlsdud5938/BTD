using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : MonoBehaviour
{
    public bool isBlocked;
    RoomInfo roombool;
    public GameObject block;
    Vector3 blockPos;
    Vector3 openPos;
    // Start is called before the first frame update
    void Start()
    {
        roombool = transform.parent.GetChild(4).transform.Find("00_Background").GetComponent<RoomInfo>();
        isBlocked = roombool.isClosed;
        block = transform.Find("block").gameObject;
        blockPos = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z);
        openPos = new Vector3(block.transform.position.x, block.transform.position.y, -10000.0f);
    }

    // Update is called once per frame
    void Update()
    {
        isBlocked = roombool.isClosed;
        if (isBlocked)
            block.transform.position = blockPos;
        else if (!isBlocked)
            block.transform.position = openPos;
    }
}
