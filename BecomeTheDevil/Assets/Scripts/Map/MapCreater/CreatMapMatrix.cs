﻿using Com.LuisPedroFonseca.ProCamera2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatMapMatrix : MonoBehaviour
{
    public class Tree
    {
        public Tree(int a, int b)
        {
            this.point[0] = a;
            this.point[1] = b;
        }
        public int[] point = new int[2];
        public int roomNum;
        public Tree r;
        public Tree l;
        public Tree t;
        public Tree b;
        public Tree parent;
        public int parentDoor =0;
        public int childDoor = 0;
        public GameObject room;
    }
    public GameObject startpoint;
    GameObject room;
    public int[,] mapMatrix = new int [30,30];
    public int roomCount = 0;
    public GameObject[] roomList;
    public GameObject spawnpoint;
    public GameObject core;
    private Queue<int[]> q = new Queue<int[]>();
    private Stack<int[]> s = new Stack<int[]>();
    private int[] enq = new int[2];
    private int[] deq = new int[2];
    private int ran;
    private int[] randomArr1 = new int[8] {2,3,6,7,10,11,14,15};    //왼쪽이 열린 방 목록
    private int[] randomArr2 = new int[8] {1,3,5,7,9,11,13,15};     //오른쪽이 열린 방 목록
    private int[] randomArr3 = new int[8] {8,9,10,11,12,13,14,15};      //윗쪽이 열린 방 목록
    private int[] randomArr4 = new int[8] {4,5,6,7,12,13,14,15};        //아래쪽이 열린 방 목록
    public Tree[] trees = new Tree[20];
    int nowRoom = 0;
    int lastRoom = 0;
    int num;
    public List<int[]> pathlist= new List<int[]>();
    public Queue<int[]> path = new Queue<int[]>();
    public GameObject mainCamera;
    ProCamera2DRooms_fix cameraRoom;
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject coreRoom;

    // Start is called before the first frame update

    void Awake()
    {
        cameraRoom = mainCamera.GetComponent<ProCamera2DRooms_fix>();
        for(int i=0;i<30;i++)
        {
            for (int j = 0; j < 30; j++)
                mapMatrix[i, j] = 0;
        }
        Tree root = new Tree(14,14);
        Tree end = new Tree(100,100);
        root.parent = end;
        trees[nowRoom] = root;
        nowRoom++;
        lastRoom++;
        mapMatrix[14, 14] = 1;
        enq[0] = 14;
        enq[1] = 14;
        roomCount = 0;
        q.Enqueue(enq);
        s.Push(enq);
        int[] ii = s.Pop();
        startpoint = Instantiate(spawnpoint, new Vector3((ii[1]-14) * 30, 0, (-ii[0]+14) * 20), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        while (roomCount < 8)
        {
            if (q.Count == 0)   //이전에 생성된 방으로 인해 맵 전체가 닫혔고 방의 갯수가 10개가 안됐을 시 이전방을 삭제하고 새로 만들어서 열릴때까지 반복(이 부분때문에 여러분 수행할 가능성이 있으나 현재로는 이대로 진행
            {
                q.Enqueue(enq);
                ran = UnityEngine.Random.Range(0, roomList.Length);
                roomCount--;
                mapMatrix[enq[0], enq[1]] = CheckTBLR(enq[0], enq[1], ran);
            }
            deq = (int[])q.Dequeue().Clone();
            if ((mapMatrix[deq[0], deq[1]] & 1) == 1)  //오른쪽이 열린방
            {
                ran = UnityEngine.Random.Range(0, 8);
                num = randomArr1[ran];
                if ((mapMatrix[deq[0], deq[1] + 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] + 1] = CheckTBLR(deq[0], deq[1] + 1, num);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[1]++;
                    q.Enqueue(en);
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 2;
                    trees[lastRoom].l = trees[nowRoom]; 
                    trees[nowRoom].r = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0], deq[1] + 1];
                    CheckTBLRLink(deq[0], deq[1] + 1, mapMatrix[deq[0], deq[1]+1], lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 2) == 2)  //왼쪽이 열린방
            {
                ran = UnityEngine.Random.Range(0, 8);
                num = randomArr2[ran];
                if ((mapMatrix[deq[0], deq[1] - 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] - 1] = CheckTBLR(deq[0], deq[1] - 1, num);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[1]--;
                    q.Enqueue(en);
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 1;
                    trees[lastRoom].r = trees[nowRoom];
                    trees[nowRoom].l = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0], deq[1] - 1];
                    CheckTBLRLink(deq[0], deq[1] - 1, mapMatrix[deq[0], deq[1]-1], lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 4) == 4)  //아래쪽이 열린방
            {
                ran = UnityEngine.Random.Range(0, 8);
                num = randomArr3[ran];
                if ((mapMatrix[deq[0] + 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] + 1, deq[1]] = CheckTBLR(deq[0] + 1, deq[1], num);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[0]++;
                    q.Enqueue(en);
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 8;
                    trees[lastRoom].b = trees[nowRoom];
                    trees[nowRoom].t = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0]+1, deq[1]];
                    CheckTBLRLink(deq[0] + 1, deq[1], mapMatrix[deq[0] + 1, deq[1]], lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 8) == 8)  //윗쪽이 열린방
            {
                ran = UnityEngine.Random.Range(0, 8);
                num = randomArr4[ran];
                if ((mapMatrix[deq[0] - 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] - 1, deq[1]] = CheckTBLR(deq[0] - 1, deq[1], num);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[0]--;
                    q.Enqueue(en);
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 4;
                    trees[lastRoom].t = trees[nowRoom];
                    trees[nowRoom].b = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0]-1, deq[1]];
                    CheckTBLRLink(deq[0] - 1, deq[1], mapMatrix[deq[0] - 1, deq[1]], lastRoom);
                    lastRoom++;
                }
            }

        }
        CloseOtherRoom();

        for(int i=0;i<30;i++)
        {
            Debug.Log(mapMatrix[i, 0] + " " + mapMatrix[i, 1] + " " + mapMatrix[i, 2] + " " + mapMatrix[i,3] + " " + mapMatrix[i, 4] + " " + mapMatrix[i, 5] + " " + mapMatrix[i, 6] + " " + mapMatrix[i, 7] + " " + mapMatrix[i, 8] + " " + mapMatrix[i,9] + " " +
                mapMatrix[i, 10] + " " + mapMatrix[i, 11] + " " + mapMatrix[i, 12] + " " + mapMatrix[i,13] + " " + mapMatrix[i,14] + " " + mapMatrix[i, 15] + " " + mapMatrix[i, 16] + " " + mapMatrix[i, 17] + " " + mapMatrix[i, 18] + " " + mapMatrix[i, 19] + " " +
                mapMatrix[i, 20] + " " + mapMatrix[i, 21] + " " + mapMatrix[i, 22] + " " + mapMatrix[i, 23] + " " + mapMatrix[i, 24] + " " + mapMatrix[i, 25] + " " + mapMatrix[i, 26] + " " + mapMatrix[i, 27] + " " + mapMatrix[i, 28] + " " + mapMatrix[i, 29] + " " );
        }
        findParent(trees[roomCount]);
        int[] com = new int[4];
        int count = path.Count;
        for (int i=0;i<count;i++)
        {
            com = (int[])path.Dequeue().Clone();
            pathlist.Add(com);
        }
        
        for (int i=0;i<30;i++)
        {
            for(int j=0;j<30;j++)
            {
                if(mapMatrix[j, i] != 0)
                {

                    room = Instantiate(roomList[mapMatrix[j,i]-1], new Vector3((i-14)* 30,0, (-j+14)* 20), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    room.GetComponent<SelectGround>().pos[0] = j;
                    room.GetComponent<SelectGround>().pos[1] = i;
                    rooms.Add(room);
                    int[] b = new int[2] { j, i };
                    room.transform.GetChild(4).Find("00_Background").GetComponent<RoomInfo>().roomPos = b;
                    for(int z =0;z<trees.Length;z++)
                    {
                        if (trees[z].point[0] == b[0] && trees[z].point[1] == b[1])
                        {
                            trees[z].room = room;
                            break;
                        }
                    }
                    for(int k=0;k<pathlist.Count;k++)
                    {
                        int[] a = new int[2] { j, i };
                        if (pathlist[k][0] == a[0] && pathlist[k][1] == a[1])
                        {
                            room.transform.GetChild(4).Find("00_Background").GetComponent<RoomInfo>().isPath = true;
                            room.transform.GetChild(4).Find("00_Background").GetComponent<RoomInfo>().parentDoor = pathlist[k][2];
                            room.transform.GetChild(4).Find("00_Background").GetComponent<RoomInfo>().childDoor = pathlist[k][3];
                        }

                    }

                    cameraRoom.AddRoom((i - 14) * 30, (-j + 14) * 20, 30, 20);
                    
                }
            }
        }
        ii = s.Pop();
        Debug.Log(ii[0].ToString() + ii[1].ToString());
        Debug.Log(trees[lastRoom-1].point[0].ToString()+ trees[lastRoom - 1].point[1].ToString());
        findRootNode(trees[lastRoom - 1]);
        findCoreRoom(ii);
        Instantiate(core, coreRoom.transform.GetChild(4).Find("SpawnPostion").Find("CorePos").gameObject.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CheckTBLR(int x, int y, int number)     //생성된 위치에서 상하좌우에 열린 길이 있는지 확인하고 열려있다면 그부분이 열린 방으로 바꾼다. 닫혔으면 닫아준다. 예전에 구현 못해서 오랜 루프를 돌게했던 부분
    {
        if ((this.mapMatrix[x, y + 1] & 2) == 2)
            number = number | 1;
        if ((this.mapMatrix[x, y - 1] & 1) == 1)
            number = number | 2;
        if ((this.mapMatrix[x + 1, y] & 8) == 8)
            number = number | 4;
        if ((this.mapMatrix[x - 1, y] & 4) == 4)
            number = number | 8;
        if (this.mapMatrix[x, y + 1] != 0 &&(this.mapMatrix[x, y + 1] & 2) == 0)
            number = number - (number & 1);
        if (this.mapMatrix[x, y - 1] != 0 && (this.mapMatrix[x, y - 1] & 1) == 0)
            number = number - (number & 2);
        if (this.mapMatrix[x + 1, y] != 0 && (this.mapMatrix[x + 1, y] & 8) == 0)
            number = number - (number & 4);
        if (this.mapMatrix[x - 1, y] != 0 && (this.mapMatrix[x - 1, y] & 4) == 0)
            number = number - (number & 8);

        roomCount ++;
        return number;
    }

    void CheckTBLRLink(int x, int y, int number, int roomnum)     //생성된 위치에서 상하좌우에 열린 길이 있는지 확인하고 열려있다면 그부분이 열린 방으로 바꾼다. 닫혔으면 닫아준다. 예전에 구현 못해서 오랜 루프를 돌게했던 부분
    {
        if ((this.mapMatrix[x, y + 1] & 2) == 2)
        {
            int i = 0;
            while (true)
            {
                if (trees[i].point[0] == x && trees[i].point[1] == y+1)
                {
                    break;
                }
                i++;
            }
            trees[roomnum].r = trees[i];
            trees[i].l = trees[roomnum];
        }
        if ((this.mapMatrix[x, y - 1] & 1) == 1)
        {
            int i = 0;
            while (true)
            {
                if (trees[i].point[0] == x && trees[i].point[1] == y - 1)
                {
                    break;
                }
                i++;
            }
            trees[roomnum].l = trees[i];
            trees[i].r = trees[roomnum];
        }
        if ((this.mapMatrix[x + 1, y] & 8) == 8)
        {
            int i = 0;
            while (true)
            {
                if (trees[i].point[0] == x+1 && trees[i].point[1] == y)
                {
                    break;
                }
                i++;
            }
            trees[roomnum].t = trees[i];
            trees[i].b = trees[roomnum];
        }
        if ((this.mapMatrix[x - 1, y] & 4) == 4)
        {
            int i = 0;
            while (true)
            {
                if (trees[i].point[0] == x-1 && trees[i].point[1] == y)
                {
                    break;
                }
                i++;
            }
            trees[roomnum].b = trees[i];
            trees[i].t = trees[roomnum];
        }
    }

    void CloseOtherRoom()      //방이 10개 이상 생성 된후 열린 부분을 닫기 위한 함수
    {
        while (q.Count != 0)
        {
            deq = (int[])q.Dequeue().Clone();

            if ((mapMatrix[deq[0], deq[1]] & 1) == 1)
            {
                if ((mapMatrix[deq[0], deq[1] + 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] + 1] = CheckTBLR(deq[0], deq[1] + 1, 0);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[1]++;
                    s.Push(en);
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 2;
                    trees[lastRoom].l = trees[nowRoom];
                    trees[nowRoom].r = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0], deq[1] + 1];
                    CheckTBLRLink(deq[0], deq[1] + 1, mapMatrix[deq[0], deq[1]+1], lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 2) == 2)
            {
                if ((mapMatrix[deq[0], deq[1] - 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] - 1] = CheckTBLR(deq[0], deq[1] - 1, 0);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[1]--;
                    s.Push(en);
                    int i = 0;
                    while(true)
                    {
                        if(trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 1;
                    trees[lastRoom].r = trees[nowRoom];
                    trees[nowRoom].l = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0], deq[1] - 1];
                    CheckTBLRLink(deq[0], deq[1] - 1, mapMatrix[deq[0], deq[1]] - 1, lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 4) == 4)
            {
                if ((mapMatrix[deq[0] + 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] + 1, deq[1]] = CheckTBLR(deq[0] + 1, deq[1], 0);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[0]++;
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 8;
                    trees[lastRoom].b = trees[nowRoom];
                    trees[nowRoom].t = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0]+1, deq[1]];
                    CheckTBLRLink(deq[0] + 1, deq[1], mapMatrix[deq[0] + 1, deq[1]], lastRoom);
                    lastRoom++;
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 8) == 8)
            {
                if ((mapMatrix[deq[0] - 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] - 1, deq[1]] = CheckTBLR(deq[0] - 1, deq[1], 0);
                    int[] en = new int[2];
                    Array.Copy(deq, en, 2);
                    en[0]--;
                    s.Push(en);
                    int i = 0;
                    while (true)
                    {
                        if (trees[i].point[0] == deq[0] && trees[i].point[1] == deq[1])
                        {
                            break;
                        }
                        i++;
                    }
                    nowRoom = i;
                    trees[lastRoom] = new Tree(en[0], en[1]);
                    trees[lastRoom].parent = trees[nowRoom];
                    trees[lastRoom].parentDoor = 4;
                    trees[lastRoom].t = trees[nowRoom];
                    trees[nowRoom].b = trees[lastRoom];
                    trees[lastRoom].roomNum = mapMatrix[deq[0]-1, deq[1]];
                    CheckTBLRLink(deq[0] - 1, deq[1], mapMatrix[deq[0] - 1, deq[1]], lastRoom);
                    lastRoom++;
                }
            }
        }
    }
    void findRootNode(Tree t)
    {
        if(t.parent == null)
        {
            Debug.Log(t.point[0].ToString() + t.point[1].ToString());
            return;
        }
        findRootNode(t.parent);
    }

    void findParent(Tree t)
    {
        int[] a=new int[4];
        a[0] = t.point[0];
        a[1] = t.point[1];
        a[2] = t.parentDoor;
        a[3] = t.childDoor;
        if (t.parent.point[0] == 100)
        {
            Debug.Log(t.point[0].ToString() + t.point[1].ToString());
            setChildDoor(t.parent, t.parentDoor);
            path.Enqueue(a);
            return;
        }
        else
        {
            setChildDoor(t.parent, t.parentDoor);
            findParent(t.parent);
        }
        Debug.Log(t.point[0].ToString() + t.point[1].ToString());
        path.Enqueue(a);
    }

    void setChildDoor(Tree t, int parentDoor)
    {
        if (parentDoor == 1)
            t.childDoor = 2;
        else if (parentDoor == 2)
            t.childDoor = 1;
        else if (parentDoor == 4)
            t.childDoor = 8;
        else if (parentDoor == 8)
            t.childDoor = 4;
    }
    void findCoreRoom(int[] ii)
    {
        for(int i=0;i<rooms.Count;i++)
        {
            if(rooms[i].GetComponent<SelectGround>().pos[0] == ii[0] && rooms[i].GetComponent<SelectGround>().pos[1] == ii[1])
            {
                coreRoom = rooms[i];
                return;
            }
        }
    }
}
