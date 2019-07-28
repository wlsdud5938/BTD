using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    static ObjectManager st;
    public static ObjectManager Call() { return st; }

    public GameObject[] origin;     // 프리팹 원본
    public List<GameObject> Manager;    // 생성된 객체들 저장할 리스트

    //public Inven IV;

    public 

    void Awake()
    {
        st = this;
        //IV = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inven>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetObject(GameObject _Obj, int _Count, string _Name)
    {
        for(int i=0; i<_Count; i++)
        {
            GameObject obj = Instantiate(_Obj) as GameObject;
            obj.transform.name = _Name;                 //이름
            obj.transform.localPosition = Vector3.zero; //위치
            obj.SetActive(false);                       //비활성화
            obj.transform.parent = transform;           //매니저 객체의 자식으로
            Manager.Add(obj);                // 리스트에 저장.
        }
    }

}
