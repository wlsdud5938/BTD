using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : MonoBehaviour
{
    public int myDoorNum;
    public bool isBlocked = true;
    RoomInfo roombool;
    public GameObject block;
    Vector3 blockPos;
    Vector3 openPos;
    // Start is called before the first frame update
    void Start()
    {
        roombool = transform.parent.GetChild(4).transform.Find("00_Background").GetComponent<RoomInfo>();

        block = transform.Find("block").gameObject;
        blockPos = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z);
        openPos = new Vector3(block.transform.position.x, block.transform.position.y, -10000.0f);
        isBlocked = true;
}

// Update is called once per frame
void Update()
    {
        if (!roombool.isClosed)
            isBlocked = false;
        if (isBlocked && !(roombool.parentDoor == myDoorNum || roombool.childDoor == myDoorNum))
            block.transform.position = blockPos;
        else
            block.transform.position = openPos;
    }
}
