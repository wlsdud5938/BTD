using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    public enum TYPE { Coin, IvItem }
    public TYPE type;
    public Sprite DefaultImg;
    public int MaxCount;

    private Inven iv;

    // Start is called before the first frame update
    void Awake()
    {
        iv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inven>();
       DefaultImg = transform.GetComponent<SpriteRenderer>().sprite;

    }
    
    void AddItem()
    {
        //Inven iv = ObjectManager.Call().IV;

        if (!iv.AddItemInSlot(this))  // 아이템 획득 실패
            Debug.Log("item is full");
        else        // 아이템 획득 성공
        {
            gameObject.SetActive(false);   // 아이템 비활성화
            Debug.Log("Additem.");
        }
    }

    private void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Player") //플레이어랑 충돌시
            AddItem();
    }

}
