using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroAI : MonoBehaviour
{
    public GameObject player;
    public Move moveAI;
    public RoomInfo curRoom;
    public GameObject core;
    public bool corePath  = true;
    public bool canAttack = false;
    public float aggroCal;
    // Start is called before the first frame update
    void Start()
    {
        moveAI = gameObject.transform.parent.GetComponent<Move>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        core = GameObject.FindGameObjectWithTag("Finish").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (corePath)
        {
            moveAI.target = core;
        }
        else
        {
            if (curRoom.unitList.Count != 0)
            {
                moveAI.target = curRoom.unitList[0];
            }
            else
                moveAI.target = player;
        }

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
