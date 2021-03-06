﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindAggroTarget : MonoBehaviour
{
    public float moveSpeed; //이동속도
    public GameObject target; //공격 타겟
    public GameObject moveTarget; //이동 타겟
    public int targetType; //타겟 타입
    public float hitCoefficient; //피격 계수
    public float collisionCoefficient; //충돌 계수
    public float skillCoefficient; //스킬 계수
    public bool hasTarget = false; //현재 타겟 유무 확인

    GameManager gm;

    public bool corePath = false;
    GameObject core;
    NavMeshAgent agent;
    public List<GameObject> enemyList;

    public RoomInfo currentRoom;
    public bool aaa;
    public bool bbb;
    AggroType aggroType;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        enemyList = new List<GameObject>();
        aggroType = GetComponent<AggroType>();
        core = GameObject.FindGameObjectWithTag("Finish").gameObject;
        if (gameObject.CompareTag("Enemy"))
            agent = GetComponent<NavMeshAgent>();
        if (gameObject.CompareTag("Enemy"))
            currentRoom = gm.rootRoom;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (enemyList.Count > 0)
            EmptyClear();

        if (gameObject.CompareTag("Enemy"))
            SaerchTarget(currentRoom.unitList);
        else if (enemyList.Count > 0)
            SaerchTarget(enemyList);
        if (gameObject.CompareTag("Enemy"))
            FindMoveTarget();
        if (target != null && !target.activeSelf)
            RemoveList(target);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "00_Background")
            currentRoom = other.gameObject.GetComponent<RoomInfo>();
    }

    public void AddList(GameObject other)
    {
        enemyList.Add(other);
    }

    public void RemoveList(GameObject other)
    {
        enemyList.Remove(other);
    }

    void SaerchTarget(List<GameObject> list)
    {
        if(enemyList.Count == 0)
        {
            target = null;
        }
        GameObject tempTarget = null;
        float tempAggroStack = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<AggroType>().aggro > tempAggroStack)
            {
                tempAggroStack = list[i].GetComponent<AggroType>().aggro;
                tempTarget = list[i].gameObject;
            }
            else if(list[i].GetComponent<AggroType>().aggro == tempAggroStack)
            {
                if(Distence(list[i], gameObject) < Distence(tempTarget, gameObject))
                {
                    tempAggroStack = list[i].GetComponent<AggroType>().aggro;
                    tempTarget = list[i].gameObject;
                }
            }
        }
        if (tempTarget == null)
            hasTarget = false;
        else
        {
            target = tempTarget;
            hasTarget = true;
        }
    }

    void EmptyClear()
    {

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                i--;

            }
        }
    }

    void FindMoveTarget()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0.0f, 0.0f);
        NavMeshPath path = new NavMeshPath();
        aaa = agent.CalculatePath(core.transform.position, path);
        bbb = path.status != NavMeshPathStatus.PathPartial;
        if (aaa && bbb)
            moveTarget = core;
        else
            moveTarget = target;
        agent.destination = new Vector3(moveTarget.transform.position.x, 0, moveTarget.transform.position.z); ;
    }

    float Distence(GameObject to, GameObject from)
    {
        return Mathf.Sqrt(Mathf.Pow(from.transform.position.x-to.transform.position.x, 2)+ Mathf.Pow(from.transform.position.z - to.transform.position.z, 2));
    }
}
