using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    static ObjectManager st;
    public static ObjectManager Call() { return st; }

    public GameObject[] origin;     // 프리팹 원본
    [HideInInspector]
    public List<GameObject> Manager;    // 생성된 객체들 저장할 리스트

    //public Inven IV;
    [HideInInspector]
    public Unit_Slots unitSlots;


    void Awake()
    {
        st = this;
        //IV = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inven>();
        //unitSlots = GameObject.FindGameObjectWithTag("Unit_Slots").GetComponent<Unit_Slots>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetObject(origin[0], 20, "GreenBullet"); // Green 총알 생성
        SetObject(origin[1], 20, "WhiteBullet"); // White 총알 생성
        SetObject(origin[2], 20, "EnemyBullet"); // White 총알 생성
    }

    private void OnDestroy()
    {
        MemoryDelete();
        st = null;
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


    public GameObject GetObject(string _Name)
    {
        if(Manager == null)
        {
            return null;
        }
        int Count = Manager.Count;
        for(int i = 0; i<Count; i++)
        {
            if (_Name != Manager[i].name) continue;

            GameObject obj = Manager[i];
            if(obj.active == true)
            {
                if(i == Count - 1)
                {
                    SetObject(obj, 1, _Name);
                    return Manager[i + 1];
                }
                continue;
            }
            return Manager[i];
        }
        return null;

    }

    public void CreateObject(string _Name, int _Count)
    {
        if (Manager == null) Manager = new List<GameObject>();

        int Count = origin.Length;
        for(int i = 0; i<Count; i++)
        {
            GameObject obj = origin[i];
            if (obj.name == _Name)
            {
                SetObject(obj, _Count, _Name);
                break;
            }
        }
    }

    public void MemoryDelete()
    {
        if (Manager == null) return;
        int Count = Manager.Count;
        for(int i = 0; i < Count; i++)
        {
            GameObject obj = Manager[i];
            GameObject.Destroy(obj);
        }
        Manager = null;
    }

}
