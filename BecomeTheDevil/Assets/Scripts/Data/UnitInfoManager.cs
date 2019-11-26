using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoManager : MonoBehaviour
{
    public UnitInfo[] unitList;     // 유닛 정보를 담은 프리팹의 리스트, 유닛 고유넘버 순서

    public static UnitInfoManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
}
