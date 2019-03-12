using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMapMatrix : MonoBehaviour
{
    public int[,] mapMatrix = new int [30,30];
    public int roomCount = 0;

    private Queue<int[]> q = new Queue<int[]>();
    private int[] enq = new int[2];
    private int[] deq = new int[2];
    private int ran;
    private int[] randomArr1 = new int[8] {2,3,6,7,10,11,14,15};
    private int[] randomArr2 = new int[8] {1,3,5,7,9,11,13,15};
    private int[] randomArr3 = new int[8] {8,9,10,11,12,13,14,15};
    private int[] randomArr4 = new int[8] {4,5,6,7,12,13,14,15};
    int num;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<30;i++)
        {
            for (int j = 0; j < 30; j++)
                mapMatrix[i, j] = 0;
        }

        mapMatrix[14, 14] = 1;

        enq[0] = 14;
        enq[1] = 14;
        roomCount = 0;
        q.Enqueue(enq);
        while(roomCount < 10)
        {
            if (q.Count == 0)
            {
                q.Enqueue(enq);
                ran = UnityEngine.Random.Range(0, 16);
                roomCount--;
                mapMatrix[enq[0], enq[1]] = CheckTBLR(enq[0], enq[1], ran); ;
            }
            deq = (int[])q.Dequeue().Clone();
            if ((mapMatrix[deq[0], deq[1]] & 1) == 1)
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
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 2) == 2)
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
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 4) == 4)
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
                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 8) == 8)
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
                }
            }

        }
        while (q.Count!=0)
        {
            deq = (int[])q.Dequeue().Clone();

            if ((mapMatrix[deq[0], deq[1]] & 1) == 1)
            {
                if ((mapMatrix[deq[0], deq[1] + 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] + 1] = CheckTBLR(deq[0], deq[1] + 1, 0);

                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 2) == 2)
            {
                if ((mapMatrix[deq[0], deq[1] - 1] == 0))
                {
                    mapMatrix[deq[0], deq[1] - 1] = CheckTBLR(deq[0], deq[1] - 1, 0);

                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 4) == 4)
            {
                if ((mapMatrix[deq[0] + 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] + 1, deq[1]] = CheckTBLR(deq[0] + 1, deq[1], 0);

                }
            }
            if ((mapMatrix[deq[0], deq[1]] & 8) == 8)
            {
                if ((mapMatrix[deq[0] - 1, deq[1]] == 0))
                {
                    mapMatrix[deq[0] - 1, deq[1]] = CheckTBLR(deq[0] - 1, deq[1], 0);

                }
            }
        }
        for (int i = 0; i < 30; i++)
        {
            Debug.Log(mapMatrix[i, 0] + " " + mapMatrix[i, 1] + " " + mapMatrix[i, 2] + " " + mapMatrix[i, 3] + " " + mapMatrix[i, 4] + " " + mapMatrix[i, 5] + " " + mapMatrix[i, 6] + " " + mapMatrix[i, 7] + " " + mapMatrix[i, 8] + " " + mapMatrix[i, 9] + " " +
                     mapMatrix[i, 10] + " " + mapMatrix[i, 11] + " " + mapMatrix[i, 12] + " " + mapMatrix[i, 13] + " " + mapMatrix[i, 14] + " " + mapMatrix[i, 15] + " " + mapMatrix[i, 16] + " " + mapMatrix[i, 17] + " " + mapMatrix[i, 18] + " " + mapMatrix[i, 19] + " " +
                     mapMatrix[i, 20] + " " + mapMatrix[i, 21] + " " + mapMatrix[i, 22] + " " + mapMatrix[i, 23] + " " + mapMatrix[i, 24] + " " + mapMatrix[i, 25] + " " + mapMatrix[i, 26] + " " + mapMatrix[i, 27] + " " + mapMatrix[i, 28] + " " + mapMatrix[i, 29] + " ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CheckTBLR(int x, int y, int number)
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
}
