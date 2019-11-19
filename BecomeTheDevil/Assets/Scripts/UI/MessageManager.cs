using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject[] messageObjects;       // 메세지 박스 오브젝트들 3개(순서 중요)
    public Text[] messageTexts;             // 메세지 텍스트 3개(순서 중요)
    public float[] mLifeCnt;

    public float mLifeTime = 2f;
    private int mCnt;

    void update()
    {
        for(int i = mCnt; i>0; i--)
        {
            mLifeCnt[i-1] += Time.deltaTime;
            if (mLifeCnt[i-1] >= mLifeTime) {
                messageObjects[i-1].SetActive(false);
                mCnt--;
            }
        }
    }

    public void ShowMessage(string message)
    {
        mCnt++;
        if (mCnt > 3) mCnt = 3;

        for(int i = mCnt; i>1; i--)
        {
            messageObjects[i - 1].SetActive(true);
            messageObjects[i - 1].transform.GetChild(0).GetComponent<Text>().text = messageObjects[i - 2].transform.GetChild(0).GetComponent<Text>().text;
            mLifeCnt[i - 1] = mLifeCnt[i - 2];
        }
        messageObjects[0].SetActive(true);
        messageObjects[0].transform.GetChild(0).GetComponent<Text>().text = message;
        mLifeCnt[0] = 0;
    }
}
