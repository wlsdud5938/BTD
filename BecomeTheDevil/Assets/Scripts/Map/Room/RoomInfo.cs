using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public bool isTestMap = false;
    public int[] roomPos = new int[2];
    int roomNum;
    CreatMapMatrix.Tree[] trees;
    public List<GameObject> unitList;
    public int maxUnit = 5;
    public int curUnitCount = 0;
    public bool isClosed = true;
    public int countFieldUnit = 0;
    public bool isPath = false;
    public int parentDoor = 0;
    public int childDoor = 0;
    bool openFirst = true;
    RandomActive gem;
    public bool isRootRoom = false;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        unitList = new List<GameObject>();

        if (isTestMap) return;

        gem = transform.parent.Find("07_Gem").GetComponent<RandomActive>();
        isClosed = true;
        trees  = GameObject.FindGameObjectWithTag("MapCreater").GetComponent<CreatMapMatrix>().trees;
    }

    // Update is called once per frame

    void LateUpdate()
    {
        if (isTestMap) return;

        ListClear();
        if(gem.gemCount == 0)
        {
            isClosed = false;
        }
        if(openFirst && !isClosed)
        {
            openFirst = false;

            for(int z=0;z<trees.Length;z++)
            {
                if (trees[z].point[0] == roomPos[0] && trees[z].point[1] == roomPos[1])
                {
                    if((trees[z].roomNum & 1) ==1)
                    {
                        trees[z].r.room.gameObject.transform.Find("02_LeftHall").GetComponent<DoorBlock>().isBlocked = false;
                    }
                    if ((trees[z].roomNum & 2) == 2)
                    {
                        trees[z].l.room.gameObject.transform.Find("02_RightHall").GetComponent<DoorBlock>().isBlocked = false;
                    }
                    if ((trees[z].roomNum & 4) == 4)
                    {
                        trees[z].t.room.gameObject.transform.Find("02_UpHall").GetComponent<DoorBlock>().isBlocked = false;
                    }
                    if ((trees[z].roomNum & 8) == 8)
                    {
                        trees[z].b.room.gameObject.transform.Find("02_DownHall").GetComponent<DoorBlock>().isBlocked = false;
                    }
                    break;
                }
            }
        }
    }

    void ListClear()
    {
        for(int i=0;i<unitList.Count;i++)
        {
            if(unitList[i] == null)
            {
                unitList.RemoveAt(i);
                i--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            isRootRoom = true;
            gameManager.rootRoom = gameObject.GetComponent<RoomInfo>();
        }
        if (other.CompareTag("Player"))
            unitList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            unitList.Remove(other.gameObject);
    }


}
