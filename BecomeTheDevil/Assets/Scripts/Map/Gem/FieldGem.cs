using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGem : MonoBehaviour
{
    public RoomInfo background;
    public GameObject spawnPos;
    public RandomActive gemcount;
    // Start is called before the first frame update
    void Start()
    {
        background = transform.parent.parent.Find("00_Background").GetComponent<RoomInfo>();
        spawnPos = transform.parent.parent.Find("SpawnPostion").gameObject;
        gemcount = transform.parent.GetComponent<RandomActive>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void die()
    {
        spawnPos.GetComponent<RandomCreate>().CreateMonster();
        gemcount.gemCount--;
        gameObject.SetActive(false);
    }

}
