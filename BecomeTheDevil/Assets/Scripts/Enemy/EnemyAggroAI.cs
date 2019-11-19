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
    public GameObject target;
    public float coreWeight = 50;
    float coreW;
    public float playerWeight = 1;
    public float unitWeight = 1;
    public int targetNum = -1;
    float maxAgg = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveAI = gameObject.transform.GetComponent<Move>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        core = GameObject.FindGameObjectWithTag("Finish").gameObject;
        coreW = coreWeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (!corePath)
        {
            coreW = 0;
        }
        else
        {
            coreW = coreWeight;
            targetNum = -1;
        }
        maxAgg = core.GetComponent<Status>().objAggroStack * coreW;
        if (curRoom != null && curRoom.unitList.Count > 0)
        {
            float[] mapUnits = new float[curRoom.unitList.Count];
            for (int i = 0; i < curRoom.unitList.Count; i++)
            {
                if (curRoom.unitList[i].GetComponent<Status>().objAggroStack * unitWeight > maxAgg)
                {
                    maxAgg = curRoom.unitList[i].GetComponent<Status>().objAggroStack * unitWeight;
                    targetNum = i;
                }
            }
        }
        if(player.GetComponent<Status>().objAggroStack * playerWeight > maxAgg)
        {
            maxAgg = player.GetComponent<Status>().objAggroStack * playerWeight;
            targetNum = -2;
        }

        switch(targetNum)
        {
            case -1:
                target = core;
                break;
            case -2:
                target = player;
                break;
            default:
                target = curRoom.unitList[targetNum];
                break;
        }

        moveAI.target = target;

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
