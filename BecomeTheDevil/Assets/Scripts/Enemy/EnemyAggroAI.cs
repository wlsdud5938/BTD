using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroAI : MonoBehaviour
{
    public GameObject player;
    public Move moveAI;
    public RoomInfo curRoom;
    // Start is called before the first frame update
    void Start()
    {
        moveAI = gameObject.transform.parent.GetComponent<Move>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if(curRoom.unitList.Count !=0)
        {
            moveAI.target = curRoom.unitList[0];
        }
        else
            moveAI.target = player;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            if(other.name== "00_Background")
                curRoom = other.gameObject.GetComponent<RoomInfo>();

        }
    }


}
