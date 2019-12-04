using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string itemName;
    public int itemIndex = 0;
    public Sprite itemSprite;
    public int value = 1;

    private Rigidbody rig;
    private Vector3 popVector = Vector3.zero;
    private static int popDirection = 0;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void Drop(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        // 터지는 연출
        popVector.x = Random.Range(0, 100);
        popVector.z = Random.Range(0, 100);

        if (itemIndex == 0)
        {
            if (popDirection == 0) popVector.x *= -1;
            else if (popDirection == 1) popVector.z *= -1;
            else if (popDirection == 2)
            {
                popVector.x *= -1;
                popVector.z *= -1;
            }
            popDirection = popDirection == 3 ? 0 : popDirection + 1;
        }
        else
        {
            if (Random.Range(0, 2) == 0) popVector.x *= -1;
            if (Random.Range(0, 2) == 0) popVector.z *= -1; 
        }


        rig.AddForce(popVector);

        StartCoroutine(Disappear());
    }

    // 일정 시간 후 사라짐
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(30.0f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.CompareTag("Player")) //플레이어랑 충돌시
        {
            gameObject.SetActive(false);
            if (itemIndex == 0) DataManager.Instance.userData_item.EarnMoney(value);    // 인덱스 0번이면(돈이면) 돈 추가
            else DataManager.Instance.userData_item.EarnItem(itemName);

            UnitSlotManager.Instance.SetSlots();
        }
    }
}
