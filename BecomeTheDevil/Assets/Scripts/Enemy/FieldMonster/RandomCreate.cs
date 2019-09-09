using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreate : MonoBehaviour
{
    int randomNum;
    public GameObject activePos;
    public GameManager gameManager;
    GameObject fieldUnit;
    GameObject newMonster;
    RoomInfo roomif;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        roomif = transform.parent.Find("00_Background").GetComponent<RoomInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateMonster()
    {
        randomNum = Random.Range(0, 3);
        activePos = gameObject.transform.GetChild(randomNum).gameObject;
        randomNum = Random.Range(0, gameManager.fieldUnit.Length);
        fieldUnit = gameManager.fieldUnit[randomNum];
        int count = activePos.transform.childCount;
        for (int i=0;i<activePos.transform.childCount;i++)
        {
            newMonster=Instantiate(fieldUnit, activePos.transform.GetChild(i).transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newMonster.GetComponent<FieldMonster>().roominfo = roomif;
            roomif.countFieldUnit++;
        }
    }
}
